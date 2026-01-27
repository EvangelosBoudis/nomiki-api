namespace Nomiki.Api.InterestRate.Commands;

/// <summary>
/// Υπολογισμός δικαιοπρακτικών τόκων και τόκων υπερημερίας.
/// </summary>
/// <param name="Amount">Ποσό.</param>
/// <param name="From">Ημ/νία (από).</param>
/// <param name="To">Ημ/νία (έως).</param>
/// <param name="CalculationMethod">Μέθοδος υπολογισμού.</param>
public record InterestCalculationCommand(
    decimal Amount,
    DateOnly From,
    DateOnly To,
    CalculationMethod CalculationMethod);

public enum CalculationMethod
{
    /// <summary>
    /// 365 ή 366 μέρες (Actual/Actual)
    /// </summary>
    CalendarYear,

    /// <summary>
    /// Σταθερό έτος 360 ημερών (συνηθισμένο σε εμπορικές συναλλαγές)
    /// </summary>
    Standard360
}