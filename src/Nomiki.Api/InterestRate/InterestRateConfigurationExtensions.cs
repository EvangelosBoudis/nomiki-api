using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nomiki.Api.InterestRate.Database;
using Nomiki.Api.InterestRate.Services;
using Nomiki.Api.Scrapper;
using Nomiki.Api.Scrapper.HtmlAgility;

namespace Nomiki.Api.InterestRate;

/// <summary>
/// Extension methods for registering interest rate services into the dependency injection container.
/// </summary>
public static class InterestRateConfigurationExtensions
{
    /// <summary>
    /// Registers all necessary services for interest rate scraping, persistence, and calculation.
    /// </summary>
    /// <param name="services">The service collection to add the services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInterestRate(this IServiceCollection services)
    {
        var configuration = services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>();

        services.Configure<InterestRateOptions>(configuration.GetSection(InterestRateOptions.Name));

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(
                configuration[$"{DatabaseOptions.Name}:{nameof(DatabaseOptions.Connection)}"]);
        });

        var migrate = configuration[$"{DatabaseOptions.Name}:{nameof(DatabaseOptions.AutoMigrate)}"];
        ArgumentNullException.ThrowIfNull(migrate);

        if (bool.Parse(migrate)) services.BuildServiceProvider().GetRequiredService<DataContext>().Database.Migrate();

        services.TryAddTransient<IScrapperClient, ScrapperClientAgility>();
        services.TryAddTransient<IInterestRateDataSourceClient, InterestRateDataSourceScrapeClient>();
        services.AddTransient<IInterestRateManager, InterestRateManager>();

        services.AddHostedService<DataReplicationHandler>();

        return services;
    }
}