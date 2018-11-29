using System;
using System.Collections.Generic;
using Management.Domain.DomainElements.BudgetPlanner;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries.User
{
    public class GetSalaryForUserWithId : Query<IEnumerable<ShiftPayment>>
    {
        public Guid UserId { get; set; }
        public DateTime FromtDate { get; }
        public DateTime ToDate { get; }

        public GetSalaryForUserWithId(Guid id, DateTime fromtDate, DateTime toDate)
        {
            UserId = id;
            FromtDate = fromtDate;
            ToDate = toDate;
        }
    }
}