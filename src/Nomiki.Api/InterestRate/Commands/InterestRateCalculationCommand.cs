namespace Nomiki.Api.InterestRate.Commands;

/// <summary>
/// Command object containing the parameters required to calculate contractual and default interest.
/// </summary>
/// <param name="Amount">The principal amount on which interest is calculated.</param>
/// <param name="From">The inclusive start date of the interest period.</param>
/// <param name="To">The inclusive end date of the interest period.</param>
/// <param name="CalculationMethod">The day-count convention to be applied.</param>
public record InterestRateCalculationCommand(
    decimal Amount,
    DateOnly From,
    DateOnly To,
    CalculationMethod CalculationMethod);

/// <summary>
/// Defines the mathematical convention used for day counting in interest calculations.
/// </summary>
public enum CalculationMethod
{
    /// <summary>
    /// Uses the actual number of days in the year (365 or 366 for leap years). 
    /// Commonly known as Actual/Actual.
    /// </summary>
    CalendarYear,

    /// <summary>
    /// Assumes a fixed year of 360 days (often used in commercial and banking transactions). 
    /// Commonly known as 30/360 or Eurobond basis.
    /// </summary>
    Standard360
}