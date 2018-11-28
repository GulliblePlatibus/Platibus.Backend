using System.Collections.Generic;
using System.Linq;
using Management.Persistence.Model;

namespace Management.Domain.DomainElements.BudgetPlanner.ValueObjects
{
    public class SortedWorkHours
    {
        public double Hours { get; }
        public Dictionary<SupplementInfo, double> SupplementHours { get; }

        public SortedWorkHours(double hours, Dictionary<SupplementInfo, double> supplementHours)
        {
            Hours = hours;
            SupplementHours = supplementHours;
        }
    }
}