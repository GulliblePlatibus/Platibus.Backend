using System.Collections;
using System.Collections.Generic;
using SimpleSoft.Mediator;
using Management.Persistence.Model;

namespace Management.Domain.Queries.Shift
{
    public class GetAllShifts : Query<IEnumerable<Persistence.Model.Shift>>
    {
            public GetAllShifts()
            {
            
            }
        }
    }
    
