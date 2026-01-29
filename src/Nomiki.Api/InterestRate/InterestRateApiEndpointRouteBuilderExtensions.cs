using System.Runtime.CompilerServices;

namespace Nomiki.Api.InterestRate;

public static class InterestRateApiEndpointRouteBuilderExtensions
{
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
            .WithSummary("Αυτόματος υπολογισμός δικαιοπρακτικών τόκων και τόκων υπερημερίας")
            .WithDescription(
                "Μπορείτε να υπολογίσετε τους τόκους επιλέγοντας ημερολογιακό έτος (=365 ημέρες και =366 ημέρες τα δίσεκτα έτη) ή επιλέγοντας λογιστικό έτος (=360 ημέρες για όλα τα έτη υπολογισμού).");

        return builder;
    }
}