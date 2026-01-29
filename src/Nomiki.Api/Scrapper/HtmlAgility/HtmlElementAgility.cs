using HtmlAgilityPack;

namespace Nomiki.Api.Scrapper.HtmlAgility;

public class HtmlElementAgility(HtmlNode node) : IHtmlElement
{
    public string GetText() => node.InnerText.Trim();

    public string GetAttribute(string name) => node.GetAttributeValue(name, string.Empty);

    public IHtmlElement QuerySelector(string xpath) => new HtmlElementAgility(node.SelectSingleNode(xpath));

    public IEnumerable<IHtmlElement> QuerySelectorAll(string xpath)
    {
        return node
            .SelectNodes(xpath)
            .Select(n => new HtmlElementAgility(n));
    }
}