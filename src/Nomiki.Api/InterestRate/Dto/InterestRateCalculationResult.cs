namespace Nomiki.Api.InterestRate.Dto;

/// <summary>
/// Represents the final result of an interest calculation, including aggregations and period breakdowns.
/// </summary>
public record InterestRateCalculationResult()
{
    /// <summary>
    /// Initializes a new instance of the result with calculated totals based on the provided periods.
    /// </summary>
    /// <param name="amount">The initial principal amount.</param>
    /// <param name="periods">The list of broken-down interest periods.</param>
    public InterestRateCalculationResult(decimal amount, List<InterestPeriodDto> periods) : this()
    {
        Amount = amount;
        Periods = periods;
        ContractualRateAmount = Periods.Sum(p => p.ContractualRate.Amount);
        DefaultRateAmount = Periods.Sum(p => p.DefaultRate.Amount);
        TotalContractualAmount = Amount + ContractualRateAmount;
        TotalDefaultAmount = Amount + DefaultRateAmount;
    }

    /// <summary>
    /// Gets the detailed breakdown of interest for each chronological sub-period.
    /// </summary>
    public List<InterestPeriodDto> Periods { get; } = [];

    /// <summary>
    /// Gets the initial principal capital amount.
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Gets the total accumulated contractual interest (Δικαιοπρακτικός) across all periods.
    /// </summary>
    public decimal ContractualRateAmount { get; }

    /// <summary>
    /// Gets the total accumulated default interest (Υπερημερίας) across all periods.
    /// </summary>
    public decimal DefaultRateAmount { get; }

    /// <summary>
    /// Gets the grand total (Principal + Total Contractual Interest).
    /// </summary>
    public decimal TotalContractualAmount { get; init; }

    /// <summary>
    /// Gets the grand total (Principal + Total Default Interest).
    /// </summary>
    public decimal TotalDefaultAmount { get; init; }
}