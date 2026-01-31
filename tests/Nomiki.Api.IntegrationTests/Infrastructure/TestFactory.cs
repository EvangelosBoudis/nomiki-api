using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Testcontainers.PostgreSql;

namespace Nomiki.Api.IntegrationTests.Infrastructure;

public class TestFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private static readonly PostgreSqlContainer Postgres = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .Build();

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "Nomiki:Database:Connection", Postgres.GetConnectionString() },
                { "Nomiki:Database:AutoMigrate", true.ToString() },
            });
        });

        return base.CreateHost(builder);
    }

    public async Task InitializeAsync()
    {
        if (Postgres.State is TestcontainersStates.Running) return;
        await Postgres.StartAsync();
    }

    public new Task DisposeAsync() => Task.CompletedTask;
}