namespace Nomiki.Api.Core;

/// <summary>
/// A base class for background services that need to execute a task at a fixed interval.
/// Uses <see cref="PeriodicTimer"/> for high-precision timing.
/// </summary>
/// <param name="logger">The logger for tracking service execution and errors.</param>
/// <param name="period">The time interval between executions.</param>
public abstract class PeriodicBackgroundService(
    ILogger<PeriodicBackgroundService> logger,
    TimeSpan period
) : BackgroundService
{
    private readonly PeriodicTimer _timer = new(period);

    /// <summary>
    /// The main execution loop of the background service.
    /// </summary>
    /// <param name="stoppingToken">Triggered when the host is shutting down.</param>
    /// <returns>A task that represents the long-running loop.</returns>
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

    /// <summary>
    /// When implemented in a derived class, contains the logic to be executed periodically.
    /// </summary>
    /// <param name="stoppingToken">Triggered when the host is shutting down.</param>
    protected abstract Task DoAsync(CancellationToken stoppingToken);
}