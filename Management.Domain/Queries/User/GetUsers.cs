using System.Collections.Generic;
using Management.Persistence.Model;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries
{
    public class GetUsers : Query<IEnumerable<Persistence.Model.User>>
    {
        

        public GetUsers()
        {
            
        }
    }
}