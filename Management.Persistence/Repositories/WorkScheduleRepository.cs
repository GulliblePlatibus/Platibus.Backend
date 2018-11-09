using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dommel;
using Management.Persistence.Model;
using Npgsql;

namespace Management.Persistence.Repositories
{


    public interface IWorkScheduleRepository : IBaseRepository<WorkSchedule>
    {
        

    }

    public class WorkScheduleRepository : BaseRepository<WorkSchedule>, IWorkScheduleRepository
    {
        public WorkScheduleRepository(IConnectionString connectionString) : base(connectionString)
        {

        }

        
    }
}