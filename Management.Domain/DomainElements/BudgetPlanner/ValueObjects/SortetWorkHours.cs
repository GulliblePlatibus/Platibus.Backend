using System.Collections.Generic;
using System.Linq;
using Management.Persistence.Model;

namespace Management.Domain.DomainElements.BudgetPlanner.ValueObjects
{
    public class SortedWorkHours
    {
        public double Hours { get; }
        public double NightHours { get; }
        public double WeekendHours { get; }
        public double NightWeekendHours { get; }

        public SortedWorkHours(double hours, double nightHours, double weekendHours, double nightWeekendHours)
        {
            Hours = hours;
            NightHours = nightHours;
            WeekendHours = weekendHours;
            NightWeekendHours = nightWeekendHours;
        }
    }
}