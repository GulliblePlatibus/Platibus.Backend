using Management.Persistence.Model;

namespace Management.Persistence.Repositories
{
    public interface IShiftRepository : IBaseRepository<Shift>
    {
        
    }
    
    public class ShiftRepository : BaseRepository<Shift>, IShiftRepository
    {
        
        
        public ShiftRepository(IConnectionString connectionString) : base(connectionString)
        {
            
        }
    }
}