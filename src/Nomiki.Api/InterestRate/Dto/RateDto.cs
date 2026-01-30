namespace Nomiki.Api.InterestRate.Dto;

/// <summary>
/// Represents specific rate details, including the percentage applied and the resulting currency amount.
/// </summary>
/// <param name="Percentage">The annual interest rate percentage.</param>
/// <param name="Amount">The calculated interest amount in currency units.</param>
public record RateDto(decimal Percentage, decimal Amount)
{
    /// <summary>
    /// Gets the annual interest rate percentage (e.g., 8.5 for 8.5%).
    /// </summary>
    public decimal Percentage { get; } = Percentage;

    /// <summary>
    /// Gets or sets the calculated interest amount for the period.
    /// </summary>
    public decimal Amount { get; set; } = Amount;

    /// <summary>
    /// Adds a specified amount to the current interest total.
    /// </summary>
    public void AddAmount(decimal amount) => Amount += amount;

    /// <summary>
    /// Rounds the interest amount to 2 decimal places using "Away From Zero" midpoint rounding 
    /// (standard financial rounding).
    /// </summary>
    public void RoundAmount() => Amount = Math.Round(Amount, 2, MidpointRounding.AwayFromZero);
}