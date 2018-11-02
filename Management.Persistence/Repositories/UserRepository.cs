using System;
using System.Threading.Tasks;
using Management.Persistence.Model;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Management.Documents.Documents;
using Management.Persistence.Helpers;

namespace Management.Persistence.Repositories
{
    public interface IUserRepository : IBaseRepository<Employee>
	{
		Task<User> GetById(Guid id);
		Task<Response> InsertUser(Employee employee);
		Task<User> Login(string email, string password);
	}

	public class UserRepository :  BaseRepository<Employee> , IUserRepository
    {
	    

	    //***********************PROPERTIES ***************************
	    
	    private static List<User> UserStore = new List<User>();

	    
	    
	    
	    //**********************CONSTRUCTOR ****************************
	    
        public UserRepository(IConnectionString connectionString) : base (connectionString)
        {
	        
        }
	    
	    // ********************* METHODS *******************************

		public async Task<User> GetById(Guid id)
		{

			return null;
		}

		public async Task<Response> InsertUser(Employee employee)
		{

			
			

			var result = InsertAsync(employee);
			
			

			if (result.Equals(1))
			{
				return Response.Unsuccessful();
			}

			return Response.Success();
		}

	    public Task<User> Login(string email, string password)
	    {
		    throw new NotImplementedException();

	    }
    }
}
