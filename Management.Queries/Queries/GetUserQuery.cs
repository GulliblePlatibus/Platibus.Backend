using System;
using SimpleSoft.Mediator;
using Management.Persistence.Model;
namespace Management.Queries.Queries
{
	public class GetUserQuery : Query<User>
    {
        public Guid UserId { get; }

		public GetUserQuery(Guid userId)
        {
            UserId = userId;
		}
    }
}
