using System;

namespace Management.Domain.DomainElements
{
    public static class DomainExtensions
    {
        public static bool DayBegan(this DateTime quarterHour, DayOfWeek dayOfWeek)
        {
            var dayWeek = quarterHour.DayOfWeek;

            if (dayWeek.Equals(dayOfWeek))
            {
                if (quarterHour.Hour == 0)
                {
                    if (quarterHour.Minute == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}