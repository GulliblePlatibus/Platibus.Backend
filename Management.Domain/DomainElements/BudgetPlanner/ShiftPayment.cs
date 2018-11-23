using System;

namespace Management.Domain.DomainElements.BudgetPlanner
{
    public class ShiftPayment
    {
        public Guid Id { get; set; }
        public Guid ShiftId { get; set; }
        public int NormalHours { get; set; }
        public double PayForNormalHours { get; set; }
        public int NightHours { get; set; }
        public double PayForNightHours { get; set; }
        public int WeekendHours { get; set; }
        public double PayForWeekendHours { get; set; }

        public double TotalPayment { get; set; }
    }
}