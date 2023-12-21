using System;
using Windows.ApplicationModel.Resources;

namespace GameBarClock.Helpers
{
    public static class DateTimeHelper
    {
        private static ResourceLoader ResourceLoader => ResourceLoader.GetForViewIndependentUse();
        public static string ToLocal(this DayOfWeek week)
        {
            switch (week)
            {
                case DayOfWeek.Sunday: return ResourceLoader.GetString("Sunday");
                case DayOfWeek.Monday: return ResourceLoader.GetString("Monday");
                case DayOfWeek.Tuesday: return ResourceLoader.GetString("Tuesday");
                case DayOfWeek.Wednesday: return ResourceLoader.GetString("Wednesday");
                case DayOfWeek.Thursday: return ResourceLoader.GetString("Thursday");
                case DayOfWeek.Friday: return ResourceLoader.GetString("Friday");
                case DayOfWeek.Saturday: return ResourceLoader.GetString("Saturday");
                default: return string.Empty;
            }
        }
    }
}
