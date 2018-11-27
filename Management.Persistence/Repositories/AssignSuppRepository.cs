using Management.Persistence.Documents;
using Management.Persistence.Model.Budget;

namespace Management.Persistence.Repositories
{
    public interface IAssignSuppRepository : IBaseRepository<AssignedSupplements>
    {
        
    }
    
    public class AssignSuppRepository : BaseRepository<AssignedSupplements>
    {
        public AssignSuppRepository(IConnectionString connectionString) : base(connectionString)
        {
            
        }
    }
}