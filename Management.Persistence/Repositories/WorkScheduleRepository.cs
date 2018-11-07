using Management.Persistence.Model;

namespace Management.Persistence.Repositories
{

    public interface IWorkScheduleRepository : IBaseRepository<WorkSchedule>
    {
        
    }
    public class WorkScheduleRepository : BaseRepository<WorkSchedule> , IWorkScheduleRepository
    {
        public WorkScheduleRepository(IConnectionString connectionString) : base(connectionString)
        {
            
        }
    }
}