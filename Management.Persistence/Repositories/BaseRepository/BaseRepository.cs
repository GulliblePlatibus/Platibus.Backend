using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dommel;
using Management.Persistence.Documents;
using Npgsql;

namespace Management.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        private readonly IConnectionString _connectionString;

        public BaseRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<object> InsertAsync(T value)
        {
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();

                
                var a = conn.Insert(value);
                Console.WriteLine(a.GetType());
                return a;
            }
        }

        public async Task<object> InsertManyAsync(IEnumerable<T> valueList)
        {
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();

                var a = conn.Insert(valueList);
                return a as T;
            }
        }

        public async Task<bool> UpdateAsync(T value)
        {
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();

                return conn.Update(value);
            }
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return null;
            }
            
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();

                return conn.Get<T>(id);
            }
        }

        public async Task<bool> DeleteByTAsync(T value)
        {
            if (value.Equals(null))
            {
                return false;
            }
            
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();

                return conn.Delete(value);
            }
        }

        public async Task<bool> DeleteManyAsync(IEnumerable<T> valueList)
        {
            if (valueList.Equals(null))
            {
                return false;
            }
            
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                conn.Open();

                return conn.Delete(valueList);
            }
        }
    }
}