namespace Nomiki.Api.InterestRate.Database;

/// <summary>
/// Represents the configuration settings for the database connection and behavior.
/// Maps to the "Nomiki:Database" section in the application settings.
/// </summary>
public record DatabaseOptions
{
    /// <summary>
    /// The section name used in configuration files (e.g., appsettings.json).
    /// </summary>
    public const string Name = "Nomiki:Database";

    /// <summary>
    /// Gets or sets the connection string used to connect to the PostgreSQL database.
    /// </summary>
    public string? Connection { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the application should automatically 
    /// apply pending migrations to the database on startup.
    /// </summary>
    public bool AutoMigrate { get; set; }
}