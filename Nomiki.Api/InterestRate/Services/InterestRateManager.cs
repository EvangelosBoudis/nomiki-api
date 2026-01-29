using Nomiki.Api.Core;
using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.InterestRate.Services;

public class InterestRateManager(IInterestRateDataSourceClient dataSourceClient) : IInterestRateManager
{
    public async Task<InterestCalculationResult> CalculateInterestAsync(InterestCalculationCommand command)
    {
        var rates = (await dataSourceClient.GetInterestRatesAsync())
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