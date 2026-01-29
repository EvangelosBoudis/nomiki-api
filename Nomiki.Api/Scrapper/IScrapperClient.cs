namespace Nomiki.Api.Scrapper;

/// <summary>
/// Service responsible for fetching and parsing web data into strongly typed objects.
/// </summary>
public interface IScrapperClient
{
    /// <summary>
    /// Scrapes a list of items from a given URL.
    /// </summary>
    /// <typeparam name="T">The type of object to return.</typeparam>
    /// <param name="url">The target website URL.</param>
    /// <param name="xpath">The XPath/Selector to find the collection of items (e.g., rows).</param>
    /// <param name="mapper">A delegate defining how to map an IHtmlElement to type T.</param>
    Task<IEnumerable<T>> ScrapeAsync<T>(string url,
        string xpath,
        Func<IHtmlElement, T> mapper);
}