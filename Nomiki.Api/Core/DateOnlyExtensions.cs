namespace Nomiki.Api.Core;

public static class DateOnlyExtensions
{
    public static DateOnly FirstDayOfYear(int year) => new(year, 1, 1);

    public static DateOnly LastDayOfYear(int year) => new(year, 12, 31);
}