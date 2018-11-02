using System;
using Management.Persistence.Model;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries
{
    public class GetUserById : Query<User>
    {
        public Guid Id { get; set; }

        public GetUserById(Guid id)
        {
            Id = id;
        }
    }
}