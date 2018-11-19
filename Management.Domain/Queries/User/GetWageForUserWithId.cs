using System;
using Management.Persistence.Model;
using Management.Persistence.Model.Budget;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries.Shift
{
    public class GetWageForUserWithId : Query<Salary>
    {
        public Guid UserId { get; private set; }

        public GetWageForUserWithId(Guid id)
        {
            UserId = id;
        }
    }
}