using System.Numerics;

namespace ConsoleApp1;

public static class Vrem
{
    public static long Unix(int year, int month, int day)
    {
        DateTime currentTime = new DateTime(year, month, day);
        return ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
    }
    
    public static int UnixTimeStampToYear( double unixTimeStamp )
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
        return dateTime.Year;
    }
}
