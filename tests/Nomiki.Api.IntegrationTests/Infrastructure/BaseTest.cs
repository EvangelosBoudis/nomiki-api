namespace Nomiki.Api.IntegrationTests.Infrastructure;

[Collection(nameof(TestFactory))]
public class BaseTest : IClassFixture<TestFactory>
{
    protected readonly HttpClient Client;

    protected BaseTest(TestFactory factory)
    {
        Client = factory.CreateClient();
    }
}