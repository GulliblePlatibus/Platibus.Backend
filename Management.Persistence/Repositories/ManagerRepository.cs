using System;
using System.Threading.Tasks;
using Management.Documents.Documents;
using Management.Persistence.Model;

namespace Management.Persistence.Repositories
{
    
    public interface IManagerRepository : IBaseRepository<Manager>
    {

        Task<Response> InsertManager(Manager manager);

    }
    
    public class ManagerRepository : BaseRepository<Manager> , IManagerRepository  
    {
        public ManagerRepository(IConnectionString connectionString) : base(connectionString)
        {
            
        }

        public Task<Response> InsertManager(Manager manager)
        {
            var result = InsertAsync(manager);

            Console.WriteLine(result);
            return null;
        }
    }
}