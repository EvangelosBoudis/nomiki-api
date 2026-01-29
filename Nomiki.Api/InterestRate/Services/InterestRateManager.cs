using Nomiki.Api.Core;
using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;
using Nomiki.Api.InterestRate.Extensions;

namespace Nomiki.Api.InterestRate.Services;

public class InterestRateManager(IInterestRateDataSourceClient dataSourceClient) : IInterestRateManager
{
    public async Task<InterestCalculationResult> CalculateInterestAsync(InterestCalculationCommand command)
    {
        var rates = (await dataSourceClient.GetInterestRatesAsync())
            .Where(r => r.From < command.To && (r.To == null || r.To >= command.From))
            .OrderBy(r => r.From);

        return new InterestCalculationResult(
            amount: command.Amount,
            periods: rates
                .SelectMany(r =>
                {
                    var start = command.From > r.From ? command.From : r.From;
                    var end = r.To == null || command.To < r.To ? command.To : r.To.Value;
                    return start < end
                        ? SplitIntoYearlyPeriods(command.Amount, command.CalculationMethod, r, start, end)
                        : [];
                }).Consolidate());
    }

    private static IEnumerable<InterestPeriodDto> SplitIntoYearlyPeriods(
        decimal amount, CalculationMethod method, InterestRateDto rate, DateOnly start, DateOnly end)
    {
        for (var i = start.Year; i <= end.Year; i++)
        {
            var startPeriod = start > DateOnlyExtensions.FirstDay(year: i)
                ? start
                : DateOnlyExtensions.FirstDay(year: i);

            var endPeriod = end < DateOnlyExtensions.LastDay(year: i) ? end : DateOnlyExtensions.LastDay(year: i);

            if (startPeriod > endPeriod) continue;

            var days = endPeriod.DayNumber - startPeriod.DayNumber + 1;

            var divisor = method == CalculationMethod.CalendarYear
                ? DateTime.IsLeapYear(i) ? 366m : 365m
                : 360m;

            yield return new InterestPeriodDto
            {
                From = startPeriod,
                To = endPeriod,
                NumOfDays = days,
                ContractualRate = new RateDto(rate.ContractualRate,
                    amount * (rate.ContractualRate / 100m) * (days / divisor)),
                DefaultRate = new RateDto(rate.DefaultRate, amount * (rate.DefaultRate / 100m) * (days / divisor))
            };
        }
    }
}