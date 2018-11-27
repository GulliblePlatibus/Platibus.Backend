using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;

namespace Management.Domain.DomainElements.BudgetPlanner
{
    public static class SalaryConfigurationExtensions
    {
        public static void UseQuarterTimeScheduling(this SalaryConfiguration salaryConfiguration)
        {
            salaryConfiguration.TimeTrackingIntervalInMinutes = 15;
        }
        
        public static void UseHalfHourTimeScheduling(this SalaryConfiguration salaryConfiguration)
        {
            salaryConfiguration.TimeTrackingIntervalInMinutes = 30;
        }
        
        public static void UseHourlyTimeScheduling(this SalaryConfiguration salaryConfiguration)
        {
            salaryConfiguration.TimeTrackingIntervalInMinutes = 60;
        }
        
        public static void UseCustomTimeScheduling(this SalaryConfiguration salaryConfiguration, int customTimeSchedulingInterval)
        {
            salaryConfiguration.TimeTrackingIntervalInMinutes = customTimeSchedulingInterval;
        }

        public static void AddSupplement(this SalaryConfiguration salaryConfiguration, SupplementInfo supplementInfo)
        {
            salaryConfiguration.AddSupplement(supplementInfo);
        }
        
        public static void ConfigureNightSupplementForWeekDays(this SalaryConfiguration salaryConfiguration, int fromHour, int toHour, double supplement)
        {
            salaryConfiguration.AddSupplement(new SupplementInfo(
                "Night Supplement",
                "Night Supplement",
                new List<DayOfWeek>{DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday},
                supplement,
                new List<HourInfo>{new HourInfo(fromHour, toHour)}));
        }

        public static void ConfigureDefaultSupplementSettings(this SalaryConfiguration salaryConfiguration)
        { 
        }

        public static void ConfigureCustomSupplementHours(this SalaryConfiguration salaryConfiguration,
            int nightHourBegin, int nightHourEnd, int weekendNightBegin, int weekendNightEnd)
        {
        }

        public static DayOfWeek PastDay(this DayOfWeek dayOfWeek)
        {
            var week = new List<DayOfWeek>
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday,
                DayOfWeek.Saturday, DayOfWeek.Sunday
            };

            var index = week.IndexOf(dayOfWeek);

            var previousIndex = index - 1;

            if (previousIndex < 0)
            {
                index = week.Count - 1;
            }

            return week[index];
        }
    }
}