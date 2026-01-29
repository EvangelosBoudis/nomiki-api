using Nomiki.Api.Core;
using Nomiki.Api.InterestRate.Services;

namespace Nomiki.Api.InterestRate;

public class DataReplicationHandler(
    ILogger<DataReplicationHandler> logger,
    IServiceProvider provider
) : PeriodicBackgroundService(logger, TimeSpan.FromMinutes(5))
{
    protected override async Task DoAsync(CancellationToken stoppingToken)
    {
        using var scope = provider.CreateScope();
        var manager = scope.ServiceProvider.GetRequiredService<IInterestRateManager>();
        await manager.ReplicateInterestRateDefinitionsAsync();
    }
}