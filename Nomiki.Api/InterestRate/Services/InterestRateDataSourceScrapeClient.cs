using System.Globalization;
using Microsoft.Extensions.Options;
using Nomiki.Api.InterestRate.Dto;
using Nomiki.Api.Scrapper;

namespace Nomiki.Api.InterestRate.Services;

public class InterestRateDataSourceScrapeClient : IInterestRateDataSourceClient
{
    private readonly string _scrapeUrl;

    private readonly IScrapperClient _scrapperClient;
    private readonly CultureInfo _culture = new("el-GR");

    public InterestRateDataSourceScrapeClient(IOptions<InterestRateOptions> options, IScrapperClient scrapperClient)
    {
        var scrapeUlr = options.Value.ScrapeUlr;
        ArgumentNullException.ThrowIfNull(scrapeUlr);
        _scrapeUrl = scrapeUlr;

        _scrapperClient = scrapperClient;
    }

    public async Task<IEnumerable<InterestRateDto>> GetInterestRatesAsync()
    {
        return await _scrapperClient.ScrapeAsync<InterestRateDto>(
            url: _scrapeUrl,
            xpath: "//table//tr[td]",
            mapper: htmlElement =>
            {
                var cells = htmlElement.QuerySelectorAll("td").ToList();

                ArgumentOutOfRangeException.ThrowIfLessThan(cells.Count, 6);

                var interestRate = new InterestRateDto
                {
                    From = DateOnly.ParseExact(cells[0].GetText(), "d/M/yyyy", _culture),
                    AdministrativeAct = cells[2].GetText(),
                    Fek = cells[3].GetText(),
                    ContractualRate = decimal.Parse(cells[4].GetText().Replace("%", ""), _culture),
                    DefaultRate = decimal.Parse(cells[5].GetText().Replace("%", ""), _culture)
                };

                var endDateText = cells[1].GetText();

                var parsed = DateOnly.TryParseExact(
                    endDateText, "d/M/yyyy", _culture, DateTimeStyles.None, out var result);
                if (parsed) interestRate.To = result;

                return interestRate;
            });
    }
}