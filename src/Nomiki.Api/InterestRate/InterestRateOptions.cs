namespace Nomiki.Api.InterestRate;

public record InterestRateOptions
{
    public const string Name = "Nomiki:InterestRate";

    public string? ScrapeUlr { get; set; }
}