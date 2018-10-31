using System;
using System.Linq;
using Dapper;
using Microsoft.IdentityModel.Protocols;
using Npgsql;

namespace Management.Persistence.Repositories

{

    public class user
    {
        public string name { get; set; }
    }
    public interface IBaseDatabase
    {
        void Insert(string value);
        void DapperTest();
    }
    public class BaseDatabase : IBaseDatabase
    {
        // 
        private readonly ElephantSQlConfig _elephantSQlConfig = null;


        public BaseDatabase()
        {
            _elephantSQlConfig = new ElephantSQlConfig();
            
        }

        public void Insert(string value)
        {
            using (var conn = new NpgsqlConnection(_elephantSQlConfig.GetConnectionString()))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * from users" , conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                        }
                    }
                }
            }
        }

        public void DapperTest()
        {
            using (var connection = new NpgsqlConnection(_elephantSQlConfig.GetConnectionString()))
            {
                connection.Open();
                
                var users = connection.Query<user>("SELECT * from users").ToList();

                Console.WriteLine(users.Count);

                foreach (var VARIABLE in users)
                {
                    Console.WriteLine(VARIABLE.name);
                }
            }
        }
        
    }
    
    
}