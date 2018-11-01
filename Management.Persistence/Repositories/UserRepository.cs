using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Management.Documents.Documents;
using Management.Persistence.Helpers;
using Management.Acquaintance;

namespace Management.Persistence.Repositories
{
    public interface IUserRepository
    {
	    Task<IUser> GetUsersAsync();
		Task<IUser> GetById(Guid id);
		Task<Response> InsertUser(IUser user);
		Task<IUser> Login(string email, string password);
	}

	public class UserRepository : BaseDatabase, IUserRepository
    {
	    // Database object
	    private readonly IBaseDatabase _baseDatabase;

	    private static List<IUser> UserStore = new List<IUser>();
	    

	    public async Task<IUser> GetUsersAsync()
	    {
            IUser result = null;
            return (result);
	    }


	    public Task<IUser> GetUsers()
	    {
		    throw new NotImplementedException();
	    }

	    public async Task<IUser> GetById(Guid id)
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

		public async Task<Response> InsertUser(IUser user)
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

	    public Task<IUser> Login(string email, string password)
	    {

		    
		    var emailList = UserStore.Where(x => x.Email.Equals(email) && BCrypt.Net.BCrypt.Verify(password, x.Password));

		    var user =  emailList.GetFirstElement();

		    return Task.FromResult(user);

	    }

	    
    }
}
