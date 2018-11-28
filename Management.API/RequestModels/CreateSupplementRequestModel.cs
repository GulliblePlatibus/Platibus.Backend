using System;
using System.Collections.Generic;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;

namespace Management.API.RequestModels
{
    public class CreateSupplementRequestModel
    {
        public string Name { get; set; }
        public string Decription { get; set; }
        public bool IsStaticSupplement { get; set; }
        public double SupplementValue { get; set; }
        public HashSet<DayOfWeek> SupplementDays { get; set; }
        public List<HourInfoRequestModel> TimeRange { get; set; }
    }

    public class HourInfoRequestModel
    {
        public int FromHour { get; set; }
        public int ToHour { get; set; }
    }
}