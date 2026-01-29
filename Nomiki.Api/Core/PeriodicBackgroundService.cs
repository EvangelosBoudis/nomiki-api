namespace Nomiki.Api.Core;

public abstract class PeriodicBackgroundService(
    ILogger<PeriodicBackgroundService> logger,
    TimeSpan period
) : BackgroundService
{
    private readonly PeriodicTimer _timer = new(period);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            try
            {
                await DoAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred while executing background task.");
            }
        } while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested);
    }

    protected abstract Task DoAsync(CancellationToken stoppingToken);
}