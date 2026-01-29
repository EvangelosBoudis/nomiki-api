using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.InterestRate.Services;

public interface IInterestRateManager
{
    Task ReplicateInterestRateDefinitionsAsync();

    Task<InterestRateCalculationResult> CalculateInterestRateAsync(InterestRateCalculationCommand command);
}