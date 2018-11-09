using System;
using System.Collections.Generic;
using SimpleSoft.Mediator;

namespace Management.Domain.Queries.WorkSchedule
{
    public class GetShiftsForUserWithId : Query<Persistence.Model.Shift>
    {
       
            public Guid Id { get; set; }


            public GetShiftsForUserWithId(Guid id)
            {
                Id = id;
            }


        }

        
    }
