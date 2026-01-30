namespace Nomiki.Api.Core;

/// <summary>
/// Provides helper methods for common <see cref="DateOnly"/> operations.
/// </summary>
public static class DateOnlyExtensions
{
    /// <summary>
    /// Returns the first day of the specified year (January 1st).
    /// </summary>
    /// <param name="year">The year for which to get the start date.</param>
    /// <returns>A <see cref="DateOnly"/> representing January 1st of the given year.</returns>
    public static DateOnly FirstDay(int year) => new(year, 1, 1);

    /// <summary>
    /// Returns the last day of the specified year (December 31st).
    /// </summary>
    /// <param name="year">The year for which to get the end date.</param>
    /// <returns>A <see cref="DateOnly"/> representing December 31st of the given year.</returns>
    public static DateOnly LastDay(int year) => new(year, 12, 31);
}