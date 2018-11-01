using System.Collections.Generic;
using Management.Domain.DomainModels.Users;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries
{
    public class GetUsers : Query<IEnumerable<User>>
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public GetUsers(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}