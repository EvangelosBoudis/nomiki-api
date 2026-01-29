using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.InterestRate.Services;

public interface IInterestRateManager
{
    Task<InterestCalculationResult> CalculateInterestAsync(InterestCalculationCommand command);
}