using Nomiki.Api.Core;
using Nomiki.Api.InterestRate.Services;

namespace Nomiki.Api.InterestRate;

/// <summary>
/// A background service that periodically triggers the interest rate data replication process.
/// Inherits from <see cref="PeriodicBackgroundService"/> to run at a fixed interval.
/// </summary>
/// <param name="logger">The logger for tracking synchronization tasks.</param>
/// <param name="provider">The service provider used to create scopes for dependency injection.</param>
public class DataReplicationHandler(
    ILogger<DataReplicationHandler> logger,
    IServiceProvider provider
) : PeriodicBackgroundService(logger, TimeSpan.FromMinutes(5))
{
    /// <summary>
    /// Executes the replication logic by creating a temporary scope to resolve the 
    /// <see cref="IInterestRateManager"/>.
    /// </summary>
    /// <param name="stoppingToken">Triggered when the application host is performing a graceful shutdown.</param>
    /// <returns>A task representing the background operation.</returns>
    protected override async Task DoAsync(CancellationToken stoppingToken)
    {
        using var scope = provider.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IInterestRateManager>();
        await manager.ReplicateInterestRateDefinitionsAsync();
    }
}