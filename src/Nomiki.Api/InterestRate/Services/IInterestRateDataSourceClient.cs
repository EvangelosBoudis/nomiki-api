using Nomiki.Api.InterestRate.Domain;

namespace Nomiki.Api.InterestRate.Services;

/// <summary>
/// Defines a client for retrieving interest rate definitions from external sources.
/// </summary>
public interface IInterestRateDataSourceClient
{
    /// <summary>
    /// Fetches all available interest rate definitions from the source.
    /// </summary>
    /// <returns>A collection of <see cref="InterestRateDefinition"/> objects.</returns>
    Task<IEnumerable<InterestRateDefinition>> GetInterestRateDefinitionsAsync();
}