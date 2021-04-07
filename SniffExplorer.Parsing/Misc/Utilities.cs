using System;

namespace SniffExplorer.Parsing.Misc
{
    public static class Utilities
    {
        private static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime GetDateTimeFromUnixTime(double unixTime)
        {
            return Epoch.AddSeconds(unixTime);
        }

        public static double GetUnixTimeFromDateTime(DateTime time)
        {
            return (time - Epoch).TotalSeconds;
        }
    }
}
