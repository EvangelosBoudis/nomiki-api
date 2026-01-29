using Microsoft.AspNetCore.Http.HttpResults;
using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;
using Nomiki.Api.InterestRate.Services;

namespace Nomiki.Api.InterestRate;

internal class InterestRateEndpoints
{
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