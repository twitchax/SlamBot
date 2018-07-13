using System;

namespace SlamBot
{
    internal static class Settings
    {
        private static readonly DateTime _startTime = DateTime.Now;

        internal static int WindowInMinutes => int.Parse(Helpers.Resolve("WINDOW_IN_MINUTES", defaultValue: Math.Min(120, (int)(DateTime.Now - _startTime).TotalMinutes).ToString()));
        internal static int RefreshRateInMinutes => int.Parse(Helpers.Resolve("REFRESH_RATE_IN_MINUTES", defaultValue: "2"));
    }
}