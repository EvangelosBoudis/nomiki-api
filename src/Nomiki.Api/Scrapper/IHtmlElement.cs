namespace Nomiki.Api.Scrapper;

/// <summary>
/// Represents a generic HTML element.
/// </summary>
public interface IHtmlElement
{
    /// <summary>
    /// Gets the trimmed inner text of the element.
    /// </summary>
    string GetText();

    /// <summary>
    /// Gets the value of a specific attribute (e.g., "href", "src").
    /// </summary>
    string GetAttribute(string name);

    /// <summary>
    /// Finds a single child element using an XPath or Selector.
    /// </summary>
    IHtmlElement QuerySelector(string xpath);

    /// <summary>
    /// Finds all child elements matching the selector.
    /// </summary>
    IEnumerable<IHtmlElement> QuerySelectorAll(string xpath);
}