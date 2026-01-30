using HtmlAgilityPack;

namespace Nomiki.Api.Scrapper.HtmlAgility;

/// <summary>
/// An implementation of <see cref="IScrapperClient"/> using HtmlAgilityPack.
/// </summary>
/// <param name="logger">The logger used for tracking scraping errors.</param>
public class ScrapperClientAgility(ILogger<ScrapperClientAgility> logger) : IScrapperClient
{
    /// <inheritdoc />
    public async Task<IEnumerable<T>> ScrapeAsync<T>(string url, string xpath, Func<IHtmlElement, T> mapper)
    {
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync(url);
        var nodes = doc.DocumentNode.SelectNodes(xpath);
        var result = new List<T>();
        foreach (var node in nodes)
        {
            try
            {
                result.Add(mapper(new HtmlElementAgility(node)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Mapping Error | URL: {Url} | Selector: {XPath} | Raw Content: {Preview}",
                    url,
                    xpath,
                    node.InnerText.Trim().Take(50));
            }
        }

        return result;
    }
}