using System;
using System.Collections.Generic;
using Management.Persistence.Model.Budget;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries
{
    public class GetWorkHoursForUser : Query<IEnumerable<Salary>>, IQuery<Salary>
    {
        public Guid Id { get; set; }
        
        public GetWorkHoursForUser(Guid id)
        {
            Id = id;
        }
    }
}