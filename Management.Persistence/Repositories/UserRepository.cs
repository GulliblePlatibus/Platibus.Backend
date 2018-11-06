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
	    
    }

	public class UserRepository : BaseRepository<User>, IUserRepository
	{


		//***********************PROPERTIES ***************************






		//**********************CONSTRUCTOR ****************************

		public UserRepository(IConnectionString connectionString) : base(connectionString)
		{

		}

		// ********************* METHODS *******************************

		
	}
}
