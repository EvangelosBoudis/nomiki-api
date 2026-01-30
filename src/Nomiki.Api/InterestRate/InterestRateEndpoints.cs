using Microsoft.AspNetCore.Http.HttpResults;
using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;
using Nomiki.Api.InterestRate.Services;

namespace Nomiki.Api.InterestRate;

/// <summary>
/// Contains the Minimal API endpoint handlers for interest rate operations.
/// </summary>
internal class InterestRateEndpoints
{
    /// <summary>
    /// Handles the HTTP GET request to calculate interest rates based on provided query parameters.
    /// </summary>
    /// <param name="manager">The interest rate manager service resolved from the DI container.</param>
    /// <param name="amount">The principal amount for the calculation.</param>
    /// <param name="from">The start date of the interest period (inclusive).</param>
    /// <param name="to">The end date of the interest period (inclusive).</param>
    /// <param name="method">The calculation convention to apply (CalendarYear or Standard360).</param>
    /// <returns>An <see cref="Ok{TValue}"/> containing the <see cref="InterestRateCalculationResult"/>.</returns>
    internal static async Task<Ok<InterestRateCalculationResult>> GetInterestRates(
        IInterestRateManager manager,
        decimal amount,
        DateOnly from,
        DateOnly to,
        CalculationMethod method)
    {
        var command = new InterestRateCalculationCommand(amount, from, to, method);
        var result = await manager.CalculateInterestRateAsync(command);
        return TypedResults.Ok(result);
    }
}