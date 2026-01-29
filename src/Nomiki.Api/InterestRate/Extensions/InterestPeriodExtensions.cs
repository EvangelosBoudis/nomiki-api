using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.InterestRate.Extensions;

public static class InterestPeriodExtensions
{
    public static List<InterestPeriodDto> Consolidate(this IEnumerable<InterestPeriodDto> periods)
    {
        var result = periods.Aggregate(new List<InterestPeriodDto>(), (acc, next) =>
        {
            var last = acc.LastOrDefault();
            if (last?.HasSameRatesWith(next) == true) last.MergeWith(next);
            else
            {
                last?.RoundRateAmounts();
                acc.Add(next);
            }

            return acc;
        });

        result.LastOrDefault()?.RoundRateAmounts();
        return result;
    }
}