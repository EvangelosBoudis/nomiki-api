namespace Nomiki.Api.InterestRate.Dto;

public class InterestRateDto
{
    /// <summary>
    /// Αρχική Ημερομηνία
    /// </summary>
    public DateOnly From { get; set; }

    /// <summary>
    /// Τελική Ημερομηνία
    /// </summary>
    public DateOnly? To { get; set; }

    /// <summary>
    /// Διοικητική Πράξη
    /// </summary>
    public required string AdministrativeAct { get; set; }

    /// <summary>
    /// Α' ΦΕΚ
    /// </summary>
    public required string Fek { get; set; }

    /// <summary>
    /// Δικαιοπρακτικός
    /// </summary>
    public decimal ContractualRate { get; set; }

    /// <summary>
    /// Υπερημερίας
    /// </summary>
    public decimal DefaultRate { get; set; }
}