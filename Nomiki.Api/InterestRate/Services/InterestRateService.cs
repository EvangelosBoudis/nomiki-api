using System.Globalization;
using Microsoft.Extensions.Options;
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
                // Get all cells (td) within the current row (tr)
                var cells = htmlElement
                    .QuerySelectorAll("td")
                    .ToList();

                ArgumentOutOfRangeException.ThrowIfLessThan(cells.Count, 6);

                var interestRate = new InterestRateDto
                {
                    // Index 0: Start Date
                    From = DateOnly.ParseExact(cells[0].GetText(), "d/M/yyyy", _culture),
                    AdministrativeAct = cells[2].GetText(),
                    Fek = cells[3].GetText(),
                    // Index 4 & 5: Rates (Removing the % symbol)
                    ContractualRate = decimal.Parse(cells[4].GetText().Replace("%", ""), _culture),
                    DefaultRate = decimal.Parse(cells[5].GetText().Replace("%", ""), _culture)
                };

                var endDateText = cells[1].GetText();

                // Index 1: End Date (Custom logic for "σήμερα")
                if (!string.IsNullOrWhiteSpace(endDateText) &&
                    !endDateText.Contains('-') &&
                    DateOnly.TryParseExact(endDateText, "d/M/yyyy", _culture, DateTimeStyles.None, out var result))
                {
                    interestRate.To = result;
                }

                return interestRate;
            });
    }

    public async Task<InterestCalculationResult> CalculateInterestAsync(InterestCalculationCommand command)
    {
        var rates = await GetInterestRatesAsync();

        var periods = new List<InterestPeriodDto>();

        // Φιλτράρισμα και εύρεση περιόδων επιτοκίων
        var relevantRates = rates
            .Where(r => r.From < command.To && (r.To == null || r.To >= command.From))
            .OrderBy(r => r.From);

        foreach (var rate in relevantRates)
        {
            var start = command.From > rate.From ? command.From : rate.From;
            var end = rate.To == null || command.To < rate.To ? command.To : rate.To.Value;

            if (start >= end) continue;

            var currentYear = start.Year;
            while (currentYear <= end.Year)
            {
                var yearStart = new DateOnly(currentYear, 1, 1);
                var yearEnd = new DateOnly(currentYear, 12, 31);

                var subStart = start > yearStart ? start : yearStart;
                var subEnd = end < yearEnd ? end : yearEnd;

                if (subStart < subEnd)
                {
                    var days = subEnd.DayNumber - subStart.DayNumber;

                    // Επιλογή διαιρέτη βάσει μεθόδου
                    var divisor = command.CalculationMethod == CalculationMethod.CalendarYear
                        ? DateTime.IsLeapYear(currentYear) ? 366m : 365m
                        : 360m;

                    // Υπολογισμός τόκων
                    var contractualAmount = command.Amount * (rate.ContractualRate / 100m) * (days / divisor);
                    var defaultAmount = command.Amount * (rate.DefaultRate / 100m) * (days / divisor);

                    var period = new InterestPeriodDto(
                        From: subStart,
                        To: subEnd,
                        NumOfDays: days,
                        ContractualRate: new RateDto(rate.ContractualRate, Math.Round(contractualAmount, 2)),
                        DefaultRate: new RateDto(rate.DefaultRate, Math.Round(defaultAmount, 2)));

                    periods.Add(period);
                }

                currentYear++;
            }
        }

        return new InterestCalculationResult
        {
            Periods = periods,
            Amount = command.Amount,
            ContractualRateAmount = periods.Sum(p => p.ContractualRate.Amount),
            DefaultRateAmount = periods.Sum(p => p.DefaultRate.Amount)
        };
    }
}