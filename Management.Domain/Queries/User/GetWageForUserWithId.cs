using System;
using Management.Domain.DomainElements.BudgetPlanner;
using Management.Persistence.Model;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries.Shift
{
    public class GetWageForUserWithId : Query<ShiftPayment>
    {
        public Guid UserId { get; private set; }

        public GetWageForUserWithId(Guid id)
        {
            UserId = id;
        }
    }
}