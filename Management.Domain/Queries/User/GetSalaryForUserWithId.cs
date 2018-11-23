using System;
using System.Collections.Generic;
using Management.Domain.DomainElements.BudgetPlanner;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries.User
{
    public class GetSalaryForUserWithId : Query<IEnumerable<ShiftPayment>>
    {
        public Guid UserId { get; set; }


        public GetSalaryForUserWithId(Guid id)
        {
            UserId = id;
        }
    }
}