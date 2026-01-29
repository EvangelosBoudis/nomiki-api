using Nomiki.Api.InterestRate.Domain;

namespace Nomiki.Api.InterestRate.Services;

public interface IInterestRateDataSourceClient
{
    Task<IEnumerable<InterestRateDefinition>> GetInterestRateDefinitionsAsync();
}