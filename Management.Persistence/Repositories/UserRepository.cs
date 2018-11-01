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
		Task<User> GetById(Guid id);
		Task<Response> InsertUser(User user);
		Task<User> Login(string email, string password);
	}

	public class UserRepository : IUserRepository
    {
	    private readonly IBaseDatabase<TestUser> _userDatabaseHandler;
	    // Database object
	    

	    private static List<User> UserStore = new List<User>();

	    
	    
        public UserRepository(IBaseDatabase<TestUser> UserDatabaseHandler)
        {
	        _userDatabaseHandler = UserDatabaseHandler;
        }

		public async Task<User> GetById(Guid id)
		{

			// Get by id
			//_userDatabaseHandler.GetById(id);
			
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

			// Insert the user to the db
			//_userDatabaseHandler.Insert(user)
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
