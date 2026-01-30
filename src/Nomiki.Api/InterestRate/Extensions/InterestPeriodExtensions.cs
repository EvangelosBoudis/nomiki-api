using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.InterestRate.Extensions;

/// <summary>
/// Provides extension methods for managing and processing collections of interest periods.
/// </summary>
public static class InterestPeriodExtensions
{
    /// <summary>
    /// Merges adjacent interest periods that share the same interest rates into a single continuous period.
    /// Also performs final rounding on the accumulated interest amounts.
    /// </summary>
    /// <param name="periods">The sequence of calculated interest periods to consolidate.</param>
    /// <returns>
    /// A list of consolidated <see cref="InterestPeriodDto"/> objects where adjacent periods 
    /// with identical rates have been merged.
    /// </returns>
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