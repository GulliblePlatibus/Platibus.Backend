using System.Collections;
using System.Collections.Generic;
using SimpleSoft.Mediator;


namespace Management.Domain.Queries
{
    public class GetAllShifts : Query<IEnumerable<Persistence.Model.Shift>>
    {
            public GetAllShifts()
            {
            
            }
        }
    }
    
