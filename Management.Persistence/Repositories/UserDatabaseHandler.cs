using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using Microsoft.IdentityModel.Protocols;
using Npgsql;

namespace Management.Persistence
{
    

    public class UserDatabaseHandler : IBaseDatabase<TestUser>
    {
        // 
        private readonly ElephantSQlConfig _elephantSQlConfig = null;


        public UserDatabaseHandler()
        {
            _elephantSQlConfig = new ElephantSQlConfig();
            
        }


        public long Insert(TestUser value)
        {
            using (var conn = new NpgsqlConnection(_elephantSQlConfig.GetConnectionString()))
            {
                conn.Open();

                return conn.Insert(value);
            }
        }

        public long InsertMany(IEnumerable<TestUser> valueList)
        {
            using (var conn = new NpgsqlConnection(_elephantSQlConfig.GetConnectionString()))
            {
                conn.Open();

                return conn.Insert(valueList);
            }
        }

        public bool Update(TestUser value)
        {
            using (var conn = new NpgsqlConnection(_elephantSQlConfig.GetConnectionString()))
            {
                conn.Open();

                return conn.Update(value);
            }
        }

        public TestUser GetById(Guid id)
        {
            if (id.Equals(null))
            {
                return null;
            }
            
            using (var conn = new NpgsqlConnection(_elephantSQlConfig.GetConnectionString()))
            {
                conn.Open();

                return conn.Get<TestUser>(id);
            }
        }

        public bool DeleteByT(TestUser value)
        {
            if (value.Equals(null))
            {
                return false;
            }
            
            using (var conn = new NpgsqlConnection(_elephantSQlConfig.GetConnectionString()))
            {
                conn.Open();

                return conn.Delete(value);
            }
        }

        public bool DeleteMany(IEnumerable<TestUser> valueList)
        {
            if (valueList.Equals(null))
            {
                return false;
            }
            
            using (var conn = new NpgsqlConnection(_elephantSQlConfig.GetConnectionString()))
            {
                conn.Open();

                return conn.Delete(valueList);
            }
            
        }
    }
    
    
}