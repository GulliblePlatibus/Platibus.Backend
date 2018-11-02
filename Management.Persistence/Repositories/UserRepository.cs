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
    public interface IUserRepository : IBaseRepository<User>
	{
		Task<User> GetById(Guid id);
		Task<Response> InsertUser(User user);
		Task<User> Login(string email, string password);
	}

	public class UserRepository :  BaseRepository<User> , IUserRepository
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

		public async Task<Response> InsertUser(User user)
		{

			var result = await DeleteByTAsync(user);

			if (result.Equals(1))
			{
				return Response.Unsuccessful();
			}

			return Response.Success();
		}

	    public Task<User> Login(string email, string password)
	    {

		    
		    
		    var emailList = UserStore.Where(x => x.Email.Equals(email) && BCrypt.Net.BCrypt.Verify(password, x.Password));

		    var user =  emailList.GetFirstElement();

		    return Task.FromResult(user);

	    }
    }
}
