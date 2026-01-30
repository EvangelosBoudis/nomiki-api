using HtmlAgilityPack;

namespace Nomiki.Api.Scrapper.HtmlAgility;

/// <summary>
/// An AgilityPack-based implementation of <see cref="IHtmlElement"/>.
/// </summary>
/// <param name="node">The underlying <see cref="HtmlNode"/> provided by HtmlAgilityPack.</param>
public class HtmlElementAgility(HtmlNode node) : IHtmlElement
{
    /// <inheritdoc />
    public string GetText() => node.InnerText.Trim();

    /// <inheritdoc />
    public string GetAttribute(string name) => node.GetAttributeValue(name, string.Empty);

    /// <inheritdoc />
    public IHtmlElement QuerySelector(string xpath) => new HtmlElementAgility(node.SelectSingleNode(xpath));

    /// <inheritdoc />
    public IEnumerable<IHtmlElement> QuerySelectorAll(string xpath)
    {
        return node
            .SelectNodes(xpath)
            .Select(n => new HtmlElementAgility(n));
    }
}