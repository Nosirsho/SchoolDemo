namespace School.Core.Helper;

public static class HelperMethods
{
    public static string ConvertTimeFromUtc(DateTime  timeUtc)
    {
        TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
        return cstTime.ToString("yyyy-MM-dd");
    }
}