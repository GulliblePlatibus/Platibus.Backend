using Management.Persistence.Model;
using Management.Persistence.Model.Budget;

namespace Management.Persistence.Repositories
{
    public interface ISupplementRepository : IBaseRepository<SupplementInfo>
    {
	   
    }

    public class SupplementRepository : BaseRepository<SupplementInfo>, ISupplementRepository
    {
        //***********************PROPERTIES ***************************

        private readonly IConnectionString connectionString;





        //**********************CONSTRUCTOR ****************************

        public SupplementRepository(IConnectionString _connectionString) : base(_connectionString)
        {
            connectionString = _connectionString;
        }

        // ********************* METHODS *******************************


		
    }
}
