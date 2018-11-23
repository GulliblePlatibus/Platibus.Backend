using System.Collections.Generic;
using System.Linq;
using Management.Persistence.Model;

namespace Management.Domain.DomainElements.BudgetPlanner.ValueObjects
{
    public class SortetWorkHours
    {
        public class SortedWorkHours
        {
            public double NormalTime { get; private set; }
            public double NightHours { get; private set; }

            public SortedWorkHours(int[] workhours)
            {
                foreach (var workHour in workhours)
                {
                    if (workHour > 6 && workHour < 18)
                    {
                        NormalTime++;
                    }
                    else
                    {
                        NightHours++;
                    }
                }
            }
        }
    }
}