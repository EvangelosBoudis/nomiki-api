namespace Nomiki.Api.InterestRate.Domain;

/// <summary>
/// Represents a specific legal interest rate period as defined by official administrative acts.
/// </summary>
public class InterestRateDefinition
{
    /// <summary>
    /// Gets or sets the unique identifier for this record.
    /// Uses GUID v4 for compatibility with .NET 8.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the effective start date of this interest rate period.
    /// </summary>
    public DateOnly From { get; set; }

    /// <summary>
    /// Gets or sets the expiration date of this interest rate period. 
    /// If <c>null</c>, the rate is considered currently active.
    /// </summary>
    public DateOnly? To { get; set; }

    /// <summary>
    /// Gets or sets the title or reference of the Administrative Act (Διοικητική Πράξη) 
    /// that established these rates.
    /// </summary>
    public required string AdministrativeAct { get; set; }

    /// <summary>
    /// Gets or sets the Government Gazette reference (ΦΕΚ) where the rates were published.
    /// </summary>
    public required string Fek { get; set; }

    /// <summary>
    /// Gets or sets the contractual interest rate (Δικαιοπρακτικός) for this period.
    /// Represented as a percentage (e.g., 7.25).
    /// </summary>
    public decimal ContractualRate { get; set; }

    /// <summary>
    /// Gets or sets the default/statutory interest rate (Υπερημερίας) for this period.
    /// Represented as a percentage (e.g., 9.25).
    /// </summary>
    public decimal DefaultRate { get; set; }

    /// <summary>
    /// Gets or sets a deterministic SHA256 hash representing the natural identity of the record.
    /// Used to detect changes during the scraping and replication process.
    /// </summary>
    public string DeterministicHash { get; set; } = string.Empty;
}