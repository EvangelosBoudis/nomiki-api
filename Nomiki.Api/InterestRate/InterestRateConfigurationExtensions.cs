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

        services.AddTransient<IScrapper, ScrapperAgility>();
        services.AddTransient<IInterestRateService, InterestRateService>();

        return services;
    }
}