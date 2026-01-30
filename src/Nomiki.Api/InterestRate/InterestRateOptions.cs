namespace Nomiki.Api.InterestRate;

/// <summary>
/// Represents the configuration settings for the Interest Rate service.
/// Maps to the "Nomiki:InterestRate" section in the application settings.
/// </summary>
public record InterestRateOptions
{
    /// <summary>
    /// The section name used in configuration files (e.g., appsettings.json).
    /// </summary>
    public const string Name = "Nomiki:InterestRate";

    /// <summary>
    /// Gets or sets the target URL for the Bank of Greece interest rate table.
    /// </summary>
    /// <value>
    /// A string containing the full URL (e.g., "https://www.bankofgreece.gr/...").
    /// </value>
    public string? ScrapeUlr { get; set; }
}