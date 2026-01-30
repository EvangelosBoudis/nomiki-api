using System.Runtime.CompilerServices;

namespace Nomiki.Api.InterestRate;

/// <summary>
/// Provides extension methods for mapping Interest Rate related API endpoints.
/// </summary>
public static class InterestRateApiEndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps the interest rate API group and defines its endpoints, OpenAPI metadata, and documentation.
    /// </summary>
    /// <param name="routes">The endpoint route builder to add the routes to.</param>
    /// <returns>A <see cref="RouteGroupBuilder"/> for further endpoint configuration.</returns>
    public static RouteGroupBuilder MapInterestRateApi(this IEndpointRouteBuilder routes)
    {
        var baseUrl = new DefaultInterpolatedStringHandler(1, 1);
        baseUrl.AppendFormatted("interest-rates");
        baseUrl.AppendLiteral("/");

        var builder = routes
            .MapGroup(prefix: baseUrl.ToStringAndClear())
            .WithOpenApi()
            .WithTags("Interest Rate");

        builder
            .MapGet(string.Empty, InterestRateEndpoints.GetInterestRates)
            .WithName("GetInterestRates")
            .WithSummary("Automatic calculation of contractual and default interest rates.")
            .WithDescription(
                "Calculate interest by selecting either the Calendar Year method (Actual/Actual - 365 days or 366 for leap years) " +
                "or the Standard 360 method (360 days for all calculation years).");

        return builder;
    }
}