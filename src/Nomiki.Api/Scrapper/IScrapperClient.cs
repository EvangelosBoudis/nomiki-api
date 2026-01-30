namespace Nomiki.Api.Scrapper;

/// <summary>
/// Service responsible for fetching and parsing web data into strongly typed objects.
/// </summary>
public interface IScrapperClient
{
    /// <summary>
    /// Scrapes a collection of items from a specified URL and maps them to a specific type.
    /// </summary>
    /// <typeparam name="T">The destination type for the scraped data.</typeparam>
    /// <param name="url">The target website URL to fetch.</param>
    /// <param name="xpath">The XPath expression used to locate the collection of elements (e.g., table rows).</param>
    /// <param name="mapper">A function delegate that defines how to transform an <see cref="IHtmlElement"/> into an instance of <typeparamref name="T"/>.</param>
    /// <returns>A task representing the asynchronous operation, containing an enumerable of mapped objects.</returns>
    /// <exception cref="HttpRequestException">Thrown when the URL cannot be reached.</exception>
    Task<IEnumerable<T>> ScrapeAsync<T>(string url,
        string xpath,
        Func<IHtmlElement, T> mapper);
}