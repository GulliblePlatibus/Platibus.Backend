using System.Collections.Generic;
using System.Linq;
using Management.Persistence.Model;

namespace Management.Domain.DomainElements.BudgetPlanner.ValueObjects
{
    public class SortedWorkHours
    {
        public double Hours { get; }
        public List<ExtendedSupplementInfo> SupplementHours { get; }

        public SortedWorkHours(double hours, List<ExtendedSupplementInfo> supplementHours)
        {
            Hours = hours;
            SupplementHours = supplementHours;
        }
    }
}