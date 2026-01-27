using HtmlAgilityPack;

namespace Nomiki.Api.Scrapper.HtmlAgility;

public class ScrapperAgility(ILogger<ScrapperAgility> logger) : IScrapper
{
    public async Task<IEnumerable<T>> ScrapeAsync<T>(string url, string xpath, Func<IHtmlElement, T> mapper)
    {
        // Initialize the web loader
        var web = new HtmlWeb();
        // Load the document from the internet
        var doc = await web.LoadFromWebAsync(url);
        // Select the parent nodes (e.g., table rows)
        var nodes = doc.DocumentNode.SelectNodes(xpath);
        // Wrap each native HtmlNode into HtmlElementAgility and map it
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