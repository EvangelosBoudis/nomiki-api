using Nomiki.Api.InterestRate.Commands;
using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.InterestRate.Services;

/// <summary>
/// Orchestrates the synchronization of interest rate data and performs complex interest calculations.
/// </summary>
public interface IInterestRateManager
{
    /// <summary>
    /// Synchronizes the local database with the external data source by inserting new records 
    /// and removing obsolete ones based on their deterministic hashes.
    /// </summary>
    Task ReplicateInterestRateDefinitionsAsync();

    /// <summary>
    /// Calculates interest for a specific amount and date range, breaking it down into chronological periods.
    /// </summary>
    /// <param name="command">The calculation parameters including amount, dates, and method.</param>
    /// <returns>A detailed result containing total amounts and period breakdowns.</returns>
    Task<InterestRateCalculationResult> CalculateInterestRateAsync(InterestRateCalculationCommand command);
}