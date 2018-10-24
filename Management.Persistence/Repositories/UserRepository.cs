using System;
using System.Threading.Tasks;
using Management.Persistence.Model;
using System.Collections.Generic;
using Management.Documents.Documents;

namespace Management.Persistence.Repositories
{
    public interface IUserRepository
	{
		Task<User> GetById(Guid id);
		Task<Response> InsertUser(User user);
	}

	public class UserRepository : IUserRepository
    {

		private static List<User> UserStore = new List<User>();

        public UserRepository()
        {
            //Do database config here!
        }

		public async Task<User> GetById(Guid id)
		{
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
			await Task.Delay(1000);
			foreach(var u in UserStore)
			{
				if(u.Id.Equals(user.Id))
				{
					return new Response(false, null, "User with id: " + user.Id + " already exists");
				}
				
			}
			
			UserStore.Add(user);

			return Response.Success();
		}
	}
}
