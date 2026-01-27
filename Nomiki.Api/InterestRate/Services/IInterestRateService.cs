using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.InterestRate.Services;

public interface IInterestRateService
{
    Task<IEnumerable<InterestRateDto>> GetInterestRatesAsync();

    Task<InterestCalculationResult> CalculateInterestAsync(InterestCalculationCommand command);
}