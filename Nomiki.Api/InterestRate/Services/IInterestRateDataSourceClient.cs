using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.InterestRate.Services;

public interface IInterestRateDataSourceClient
{
    Task<IEnumerable<InterestRateDto>> GetInterestRatesAsync();
}