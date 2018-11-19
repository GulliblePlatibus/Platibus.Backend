using System;
using Management.Persistence.Model.Budget;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries.User
{
    public class GetSalaryForUserWithId : Query<Salary>
    {
        public Guid UserId { get; private set; }


        public GetSalaryForUserWithId(Guid id)
        {
            UserId = id;
        }
    }
}