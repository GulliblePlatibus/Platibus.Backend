using System;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;

namespace Management.Domain.DomainElements.BudgetPlanner
{
    public class ShiftPayment
    {
        public Guid UserId { get; }
        public Guid ShiftId { get; }
        public SortedWorkHours SortedWorkHours { get; }

        public ShiftPayment(Guid userId, Guid shiftId, SortedWorkHours sortedWorkHours)
        {
            UserId = userId;
            ShiftId = shiftId;
            SortedWorkHours = sortedWorkHours;
        }
        
        
    }
}