namespace Nomiki.Api.InterestRate.Dto;

public record InterestCalculationResult
{
    /// <summary>
    /// Λεπτομέριες ανά διάστημα
    /// </summary>
    public List<InterestPeriodDto> Periods { get; init; } = [];

    /// <summary>
    /// Αρχικό Κεφάλαιο
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    /// Δικαιοπρακτικός τόκος
    /// </summary>
    public decimal ContractualRateAmount { get; init; }

    /// <summary>
    /// Τόκος υπερημερίας
    /// </summary>
    public decimal DefaultRateAmount { get; init; }

    /// <summary>
    /// Σύνολο (Δικαιοπρακτικός τόκος) 
    /// </summary>
    public decimal TotalContractualAmount => Amount + ContractualRateAmount;

    /// <summary>
    /// Σύνολο (Τόκος υπερημερίας) 
    /// </summary>
    public decimal TotalDefaultAmount => Amount + DefaultRateAmount;
}

/// <summary>
/// Represents the interest period.
/// </summary>
/// <param name="From">Ημ/νία (από).</param>
/// <param name="To">Ημ/νία (έως).</param>
/// <param name="NumOfDays">Ημέρες.</param>
/// <param name="ContractualRate">Δικαιοπρακτικός τόκος.</param>
/// <param name="DefaultRate">Τόκος υπερημερίας.</param>
public record InterestPeriodDto(
    DateOnly From,
    DateOnly To,
    int NumOfDays,
    RateDto ContractualRate,
    RateDto DefaultRate);

/// <summary>
/// Represents the rate details for a interest period.
/// </summary>
/// <param name="Percentage">Επιτόκιο.</param>
/// <param name="Amount">Τόκος.</param>
public record RateDto(decimal Percentage, decimal Amount);