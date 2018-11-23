using System;
using System.Collections.Generic;
using Management.Domain.DomainElements.BudgetPlanner;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries
{
    public class GetWorkHoursForUser : Query<IEnumerable<ShiftPayment>>, IQuery<ShiftPayment>
    {
        public Guid Id { get; set; }
        
        public GetWorkHoursForUser(Guid id)
        {
            Id = id;
        }
    }
}