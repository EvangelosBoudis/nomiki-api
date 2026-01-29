using Microsoft.Extensions.DependencyInjection.Extensions;
using Nomiki.Api.InterestRate.Services;
using Nomiki.Api.Scrapper;
using Nomiki.Api.Scrapper.HtmlAgility;

namespace Nomiki.Api.InterestRate;

public static class InterestRateConfigurationExtensions
{
    public static IServiceCollection AddInterestRate(this IServiceCollection services)
    {
        var configuration = services
            .BuildServiceProvider()
            .GetRequiredService<IConfiguration>();

        services.Configure<InterestRateOptions>(configuration.GetSection(InterestRateOptions.Name));

        services.TryAddTransient<IScrapperClient, ScrapperClientAgility>();

        services.TryAddTransient<IInterestRateDataSourceClient, InterestRateDataSourceScrapeClient>();
        services.AddTransient<IInterestRateManager, InterestRateManager>();

        return services;
    }
}