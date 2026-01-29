namespace Nomiki.Api.Core;

public static class DateOnlyExtensions
{
    public static DateOnly FirstDay(int year) => new(year, 1, 1);

    public static DateOnly LastDay(int year) => new(year, 12, 31);
}