using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Management.Persistence.Repositories
{
    public class ElephantSQlConfig : IConnectionString
    {
        // The connection string used to connect to database
        public string _connectionstring;

        public ElephantSQlConfig()
        {
            var db = "aljmjifs";
            var user = "aljmjifs";
            var passwd = "QQOlIqoOPexGnza7GIuGx6sqColJeoeH";
            var port =  5432;
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                "baasu.db.elephantsql.com", db, user, passwd, port);
            _connectionstring = connStr;
        }

        public string GetConnectionString()
        {
            return _connectionstring;
        }
    }
}