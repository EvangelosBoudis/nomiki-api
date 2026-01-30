namespace Nomiki.Api.Scrapper;

/// <summary>
/// Represents a generic HTML element, providing an abstraction over specific HTML parsing libraries.
/// </summary>
public interface IHtmlElement
{
    /// <summary>
    /// Gets the inner text of the element with leading and trailing whitespace removed.
    /// </summary>
    /// <returns>A trimmed string of the inner content, or an empty string if no content exists.</returns>
    string GetText();

    /// <summary>
    /// Gets the value of a specific HTML attribute.
    /// </summary>
    /// <param name="name">The name of the attribute (e.g., "href", "class", "src").</param>
    /// <returns>The attribute value if present; otherwise, an empty string.</returns>
    string GetAttribute(string name);

    /// <summary>
    /// Finds the first child element that matches the specified XPath expression.
    /// </summary>
    /// <param name="xpath">The XPath query used to locate the element.</param>
    /// <returns>The first matching <see cref="IHtmlElement"/>, or <c>null</c> if no match is found.</returns>
    IHtmlElement QuerySelector(string xpath);

    /// <summary>
    /// Finds all child elements that match the specified XPath expression.
    /// </summary>
    /// <param name="xpath">The XPath query used to locate the elements.</param>
    /// <returns>An enumerable collection of matching <see cref="IHtmlElement"/> objects.</returns>
    IEnumerable<IHtmlElement> QuerySelectorAll(string xpath);
}