namespace Nomiki.Api.InterestRate.Dto;

/// <summary>
/// Data Transfer Object representing interest details for a specific time interval.
/// </summary>
public record InterestPeriodDto
{
    /// <summary>
    /// Gets or sets the start date of the period.
    /// </summary>
    public DateOnly From { get; set; }

    /// <summary>
    /// Gets or sets the end date of the period.
    /// </summary>
    public DateOnly To { get; set; }

    /// <summary>
    /// Gets or sets the number of days calculated within this period.
    /// </summary>
    public int NumOfDays { get; set; }

    /// <summary>
    /// Gets or sets the contractual interest details (Δικαιοπρακτικός) for this period.
    /// </summary>
    public required RateDto ContractualRate { get; set; }

    /// <summary>
    /// Gets or sets the default/statutory interest details (Υπερημερίας) for this period.
    /// </summary>
    public required RateDto DefaultRate { get; set; }

    /// <summary>
    /// Increments the total number of days for this period.
    /// </summary>
    private void AddNumOfDays(int days) => NumOfDays += days;

    /// <summary>
    /// Determines if this period has the same interest percentages as another period.
    /// Used for merging adjacent time intervals.
    /// </summary>
    public bool HasSameRatesWith(InterestPeriodDto other) =>
        ContractualRate.Percentage == other.ContractualRate.Percentage &&
        DefaultRate.Percentage == other.DefaultRate.Percentage;

    /// <summary>
    /// Merges the current period with a subsequent one, updating dates and accumulating amounts.
    /// </summary>
    /// <param name="other">The adjacent period to merge into this one.</param>
    public void MergeWith(InterestPeriodDto other)
    {
        To = other.To;
        AddNumOfDays(other.NumOfDays);
        ContractualRate.AddAmount(other.ContractualRate.Amount);
        DefaultRate.AddAmount(other.DefaultRate.Amount);
    }

    /// <summary>
    /// Rounds the accumulated interest amounts to two decimal places.
    /// </summary>
    public void RoundRateAmounts()
    {
        ContractualRate.RoundAmount();
        DefaultRate.RoundAmount();
    }
}