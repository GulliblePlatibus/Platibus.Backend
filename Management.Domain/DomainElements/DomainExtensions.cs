using System;
using System.Collections.Generic;
using System.Linq;

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

        public static bool DayBegan(this DateTime quarterHour)
        {
            if (quarterHour.Hour == 0)
            {
                if (quarterHour.Minute == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static void SubstractMinute(this DateTime dateTime, int minutes)
        {
            var timeSpan = new TimeSpan(0, minutes, 0);

            dateTime.Subtract(timeSpan);
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            if (list.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}