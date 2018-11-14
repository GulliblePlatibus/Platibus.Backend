using System;
using System.Collections.Generic;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries
{
    public class GetSalaryForUser : Query<IEnumerable<Persistence.Model.Shift>>
    {
        public Guid Id { get; set; }
        
        public GetSalaryForUser(Guid id)
        {
            Id = id;
        }
    }
}