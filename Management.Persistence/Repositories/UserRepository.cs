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
    public interface IUserRepository
    {
	    Task<User> GetUsersAsync();
		Task<User> GetById(Guid id);
		Task<Response> InsertUser(User user);
		Task<User> Login(string email, string password);
	}

	public class UserRepository : BaseDatabase, IUserRepository
    {
	    // Database object
	    private readonly IBaseDatabase _baseDatabase;

	    private static List<User> UserStore = new List<User>();
	    

	    public async Task<User> GetUsersAsync()
	    {
            User result = null;
            return (result);
	    }


	    public Task<User> GetUsers()
	    {
		    throw new NotImplementedException();
	    }

	    public async Task<User> GetById(Guid id)
		{

			// Insert to DB
			//_baseDatabase.Insert("INSER TO DB");
			
			await Task.Delay(1000);

			foreach(var user in UserStore)
			{
				if(user.Id.Equals(id))
				{
					return user;
				}
			}
			return null;
		}

		public async Task<Response> InsertUser(User user)
		{
			//Insert();
			
			var result = UserStore.Where(x => x.Email.Equals(user.Email));

			if (result.Any())
			{
				return new Response(false, null, "User with email: " + user.Email + " already exists");
			}

			UserStore.Add(user);

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
