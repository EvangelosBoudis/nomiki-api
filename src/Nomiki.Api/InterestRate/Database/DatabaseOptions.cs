namespace Nomiki.Api.InterestRate.Database;

public record DatabaseOptions
{
    public const string Name = "Nomiki:Database";

    public string? Connection { get; set; }

    public bool AutoMigrate { get; set; }
}