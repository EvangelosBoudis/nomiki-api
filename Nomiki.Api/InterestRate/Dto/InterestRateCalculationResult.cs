namespace Nomiki.Api.InterestRate.Dto;

public record InterestRateCalculationResult()
{
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
    /// Λεπτομέριες ανά διάστημα
    /// </summary>
    public List<InterestPeriodDto> Periods { get; } = [];

    /// <summary>
    /// Αρχικό Κεφάλαιο
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// Δικαιοπρακτικός τόκος
    /// </summary>
    public decimal ContractualRateAmount { get; }

    /// <summary>
    /// Τόκος υπερημερίας
    /// </summary>
    public decimal DefaultRateAmount { get; }

    /// <summary>
    /// Σύνολο (Δικαιοπρακτικός τόκος) 
    /// </summary>
    public decimal TotalContractualAmount { get; init; }

    /// <summary>
    /// Σύνολο (Τόκος υπερημερίας) 
    /// </summary>
    public decimal TotalDefaultAmount { get; init; }
}

/// <summary>
/// Represents the interest period.
/// </summary>
public record InterestPeriodDto
{
    /// <summary>
    /// Ημ/νία (από).
    /// </summary>
    public DateOnly From { get; set; }

    /// <summary>
    /// Ημ/νία (έως).
    /// </summary>
    public DateOnly To { get; set; }

    /// <summary>
    /// Ημέρες.
    /// </summary>
    public int NumOfDays { get; set; }

    /// <summary>
    /// Δικαιοπρακτικός τόκος.
    /// </summary>
    public required RateDto ContractualRate { get; set; }

    /// <summary>
    /// Τόκος υπερημερίας.
    /// </summary>
    public required RateDto DefaultRate { get; set; }

    private void AddNumOfDays(int days) => NumOfDays += days;

    public bool HasSameRatesWith(InterestPeriodDto other) =>
        ContractualRate.Percentage == other.ContractualRate.Percentage &&
        DefaultRate.Percentage == other.DefaultRate.Percentage;

    public void MergeWith(InterestPeriodDto other)
    {
        To = other.To;
        AddNumOfDays(other.NumOfDays);
        ContractualRate.AddAmount(other.ContractualRate.Amount);
        DefaultRate.AddAmount(other.DefaultRate.Amount);
    }

    public void RoundRateAmounts()
    {
        ContractualRate.RoundAmount();
        DefaultRate.RoundAmount();
    }
}

/// <summary>
/// Represents the rate details for a interest period.
/// </summary>
/// <param name="Percentage">Επιτόκιο.</param>
/// <param name="Amount">Τόκος.</param>
public record RateDto(decimal Percentage, decimal Amount)
{
    /// <summary>
    /// Επιτόκιο.
    /// </summary>
    public decimal Percentage { get; } = Percentage;

    /// <summary>
    /// Τόκος.
    /// </summary>
    public decimal Amount { get; set; } = Amount;

    public void AddAmount(decimal amount) => Amount += amount;

    public void RoundAmount() => Amount = Math.Round(Amount, 2, MidpointRounding.AwayFromZero);
}