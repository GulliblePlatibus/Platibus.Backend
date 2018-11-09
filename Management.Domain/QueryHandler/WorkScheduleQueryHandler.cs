using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management.Domain.Queries;
using Management.Domain.Queries.Shift;
using Management.Domain.Queries.WorkSchedule;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.QueryHandler
{
    public class WorkScheduleQueryHandler
    {
        public IWorkScheduleRepository WorkScheduleRepository { get; }
        

        public WorkScheduleQueryHandler(IWorkScheduleRepository _workScheduleRepository)
        {
            WorkScheduleRepository = _workScheduleRepository;
        }
        
        
        
    }
}
