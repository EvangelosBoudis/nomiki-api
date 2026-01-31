using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Nomiki.Api.IntegrationTests.Infrastructure;
using Nomiki.Api.InterestRate.Dto;

namespace Nomiki.Api.IntegrationTests;

public class InterestRateTest(TestFactory factory) : BaseTest(factory)
{
    [Fact]
    public async Task GetInterestRates_ShouldReturnResultAnd200OK()
    {
        var response = await Client.GetAsync("interest-rates?amount=1000.50&from=2024-01-01&to=2025-12-31&method=0");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<InterestRateCalculationResult>();

        // Validate Header Totals
        result!.Amount.Should().Be(1000.50m);
        result.ContractualRateAmount.Should().Be(170.48m);
        result.DefaultRateAmount.Should().Be(210.52m);
        result.TotalContractualAmount.Should().Be(1170.98m);
        result.TotalDefaultAmount.Should().Be(1211.02m);

        // Validate Periods Collection
        result.Periods.Should().HaveCount(9);

        // Period 1: 2024-01-01 to 2024-06-11
        result.Periods[0].From.Should().Be(new DateOnly(2024, 1, 1));
        result.Periods[0].To.Should().Be(new DateOnly(2024, 6, 11));
        result.Periods[0].NumOfDays.Should().Be(163);
        result.Periods[0].ContractualRate.Percentage.Should().Be(9.75m);
        result.Periods[0].ContractualRate.Amount.Should().Be(43.44m);
        result.Periods[0].DefaultRate.Percentage.Should().Be(11.75m);
        result.Periods[0].DefaultRate.Amount.Should().Be(52.36m);

        // Period 2: 2024-06-12 to 2024-09-17
        result.Periods[1].From.Should().Be(new DateOnly(2024, 6, 12));
        result.Periods[1].To.Should().Be(new DateOnly(2024, 9, 17));
        result.Periods[1].NumOfDays.Should().Be(98);
        result.Periods[1].ContractualRate.Percentage.Should().Be(9.50m);
        result.Periods[1].ContractualRate.Amount.Should().Be(25.45m);
        result.Periods[1].DefaultRate.Percentage.Should().Be(11.50m);
        result.Periods[1].DefaultRate.Amount.Should().Be(30.81m);

        // Period 3: 2024-09-18 to 2024-10-22
        result.Periods[2].From.Should().Be(new DateOnly(2024, 9, 18));
        result.Periods[2].To.Should().Be(new DateOnly(2024, 10, 22));
        result.Periods[2].NumOfDays.Should().Be(35);
        result.Periods[2].ContractualRate.Percentage.Should().Be(8.90m);
        result.Periods[2].ContractualRate.Amount.Should().Be(8.52m);
        result.Periods[2].DefaultRate.Percentage.Should().Be(10.90m);
        result.Periods[2].DefaultRate.Amount.Should().Be(10.43m);

        // Period 4: 2024-10-23 to 2024-12-17
        result.Periods[3].From.Should().Be(new DateOnly(2024, 10, 23));
        result.Periods[3].To.Should().Be(new DateOnly(2024, 12, 17));
        result.Periods[3].NumOfDays.Should().Be(56);
        result.Periods[3].ContractualRate.Percentage.Should().Be(8.65m);
        result.Periods[3].ContractualRate.Amount.Should().Be(13.24m);
        result.Periods[3].DefaultRate.Percentage.Should().Be(10.65m);
        result.Periods[3].DefaultRate.Amount.Should().Be(16.30m);

        // Period 5: 2024-12-18 to 2025-02-04
        result.Periods[4].From.Should().Be(new DateOnly(2024, 12, 18));
        result.Periods[4].To.Should().Be(new DateOnly(2025, 2, 4));
        result.Periods[4].NumOfDays.Should().Be(49);
        result.Periods[4].ContractualRate.Percentage.Should().Be(8.40m);
        result.Periods[4].ContractualRate.Amount.Should().Be(11.27m);
        result.Periods[4].DefaultRate.Percentage.Should().Be(10.40m);
        result.Periods[4].DefaultRate.Amount.Should().Be(13.96m);

        // Period 6: 2025-02-05 to 2025-03-11
        result.Periods[5].From.Should().Be(new DateOnly(2025, 2, 5));
        result.Periods[5].To.Should().Be(new DateOnly(2025, 3, 11));
        result.Periods[5].NumOfDays.Should().Be(35);
        result.Periods[5].ContractualRate.Percentage.Should().Be(8.15m);
        result.Periods[5].ContractualRate.Amount.Should().Be(7.82m);
        result.Periods[5].DefaultRate.Percentage.Should().Be(10.15m);
        result.Periods[5].DefaultRate.Amount.Should().Be(9.74m);

        // Period 7: 2025-03-12 to 2025-04-22
        result.Periods[6].From.Should().Be(new DateOnly(2025, 3, 12));
        result.Periods[6].To.Should().Be(new DateOnly(2025, 4, 22));
        result.Periods[6].NumOfDays.Should().Be(42);
        result.Periods[6].ContractualRate.Percentage.Should().Be(7.90m);
        result.Periods[6].ContractualRate.Amount.Should().Be(9.09m);
        result.Periods[6].DefaultRate.Percentage.Should().Be(9.90m);
        result.Periods[6].DefaultRate.Amount.Should().Be(11.40m);

        // Period 8: 2025-04-23 to 2025-06-10
        result.Periods[7].From.Should().Be(new DateOnly(2025, 4, 23));
        result.Periods[7].To.Should().Be(new DateOnly(2025, 6, 10));
        result.Periods[7].NumOfDays.Should().Be(49);
        result.Periods[7].ContractualRate.Percentage.Should().Be(7.65m);
        result.Periods[7].ContractualRate.Amount.Should().Be(10.27m);
        result.Periods[7].DefaultRate.Percentage.Should().Be(9.65m);
        result.Periods[7].DefaultRate.Amount.Should().Be(12.96m);

        // Period 9: 2025-06-11 to 2025-12-31
        result.Periods[8].From.Should().Be(new DateOnly(2025, 6, 11));
        result.Periods[8].To.Should().Be(new DateOnly(2025, 12, 31));
        result.Periods[8].NumOfDays.Should().Be(204);
        result.Periods[8].ContractualRate.Percentage.Should().Be(7.40m);
        result.Periods[8].ContractualRate.Amount.Should().Be(41.38m);
        result.Periods[8].DefaultRate.Percentage.Should().Be(9.40m);
        result.Periods[8].DefaultRate.Amount.Should().Be(52.56m);
    }
}