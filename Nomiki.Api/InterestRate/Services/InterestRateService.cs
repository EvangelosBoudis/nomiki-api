using System.Globalization;
using Microsoft.Extensions.Options;
using Nomiki.Api.Core;
using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;
using Nomiki.Api.Scrapper;

namespace Nomiki.Api.InterestRate.Services;

public class InterestRateService : IInterestRateService
{
    private readonly string _scrapeUrl;

    private readonly IScrapper _scrapper;
    private readonly CultureInfo _culture = new("el-GR");

    public InterestRateService(IOptions<InterestRateOptions> options, IScrapper scrapper)
    {
        var scrapeUlr = options.Value.ScrapeUlr;
        ArgumentNullException.ThrowIfNull(scrapeUlr);
        _scrapeUrl = scrapeUlr;

        _scrapper = scrapper;
    }

    public async Task<IEnumerable<InterestRateDto>> GetInterestRatesAsync()
    {
        return await _scrapper.ScrapeAsync<InterestRateDto>(
            url: _scrapeUrl,
            xpath: "//table//tr[td]",
            mapper: htmlElement =>
            {
                var cells = htmlElement
                    .QuerySelectorAll("td")
                    .ToList();

                ArgumentOutOfRangeException.ThrowIfLessThan(cells.Count, 6);

                var interestRate = new InterestRateDto
                {
                    From = DateOnly.ParseExact(cells[0].GetText(), "d/M/yyyy", _culture),
                    AdministrativeAct = cells[2].GetText(),
                    Fek = cells[3].GetText(),
                    ContractualRate = decimal.Parse(cells[4].GetText().Replace("%", ""), _culture),
                    DefaultRate = decimal.Parse(cells[5].GetText().Replace("%", ""), _culture)
                };

                var endDateText = cells[1].GetText();

                if (!string.IsNullOrWhiteSpace(endDateText) &&
                    !endDateText.Contains('-') &&
                    DateOnly.TryParseExact(endDateText, "d/M/yyyy", _culture, DateTimeStyles.None, out var result))
                    interestRate.To = result;

                return interestRate;
            });
    }

    public async Task<InterestCalculationResult> CalculateInterestAsync(InterestCalculationCommand command)
    {
        var rates = (await GetInterestRatesAsync())
            .Where(r => r.From < command.To && (r.To == null || r.To >= command.From))
            .OrderBy(r => r.From);

        var periods = new List<InterestPeriodDto>();

        foreach (var rate in rates)
        {
            var start = command.From > rate.From ? command.From : rate.From;
            var end = rate.To == null || command.To < rate.To ? command.To : rate.To.Value;

            if (start >= end) continue;

            for (var i = start.Year; i <= end.Year; i++)
            {
                var startYear = DateOnlyExtensions.FirstDayOfYear(i);
                var endYear = DateOnlyExtensions.LastDayOfYear(i);

                var startSub = start > startYear ? start : startYear;
                var endSub = end < endYear ? end : endYear;

                if (startSub > endSub) continue;

                var days = endSub.DayNumber - startSub.DayNumber + 1;
                var divisor = command.CalculationMethod == CalculationMethod.CalendarYear
                    ? DateTime.IsLeapYear(i) ? 366m : 365m
                    : 360m;

                var period = new InterestPeriodDto
                {
                    From = startSub,
                    To = endSub,
                    NumOfDays = days,
                    ContractualRate = new RateDto(
                        Percentage: rate.ContractualRate,
                        Amount: command.Amount * (rate.ContractualRate / 100m) * (days / divisor)),
                    DefaultRate = new RateDto(
                        Percentage: rate.DefaultRate,
                        Amount: command.Amount * (rate.DefaultRate / 100m) * (days / divisor))
                };

                periods.Add(period);
            }
        }

        var merged = periods
            .Aggregate(new List<InterestPeriodDto>(), (l, n) =>
            {
                var last = l.LastOrDefault();
                if (last?.HasSameRatesWith(n) == true) last.MergeWith(n);
                else
                {
                    last?.RoundRateAmounts();
                    l.Add(n);
                }

                return l;
            });

        merged.LastOrDefault()?.RoundRateAmounts();

        return new InterestCalculationResult(command.Amount, merged);
    }
}