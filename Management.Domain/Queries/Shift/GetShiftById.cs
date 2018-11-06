using System;
using Management.Persistence.Model;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries.Shift
{
    public class GetShiftById : Query<Persistence.Model.Shift>, IQuery<User>
    {
        public Guid Id { get; set; }

        public GetShiftById(Guid id)
        {
            Id = id;
        }
    }
}
