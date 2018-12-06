using System;
using System.Threading.Tasks;
using Management.Persistence.Model;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Dapper;
using Management.Documents.Documents;
using Management.Persistence.Helpers;
using Npgsql;

namespace Management.Persistence.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
	   
    }

	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		//***********************PROPERTIES ***************************

		private readonly IConnectionString connectionString;





		//**********************CONSTRUCTOR ****************************

		public UserRepository(IConnectionString _connectionString) : base(_connectionString)
		{
			connectionString = _connectionString;
		}

		// ********************* METHODS *******************************


		
	}
}
