using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dommel;
using Management.Documents.Documents;
using Management.Persistence.Model;
using Npgsql;

namespace Management.Persistence.Repositories
{
    public interface IShiftRepository : IBaseRepository<Shift>
    {
        Task<IEnumerable<Shift>> GetForUserWithIdAsync(Guid Id);
    }
    
    
    public class ShiftRepository : BaseRepository<Shift>, IShiftRepository
    {
        
        private readonly IConnectionString ConnectionString;
        
        public ShiftRepository(IConnectionString _connectionString) : base(_connectionString)
        {
            ConnectionString = _connectionString;
        }

        public async Task<IEnumerable<Shift>> GetForUserWithIdAsync(Guid id)
        {
            
             
                using (var conn = NpgsqlConnection(ConnectionString.GetConnectionString()))
                {
                    conn.Open();
                    
                    var result = conn.QueryAsync<Shift>("Select * from shifts where Id in --> (Select shiftId from hasShift where employeeId =" + id); //TODO : <-- Discuss SQL injection attack
                    
                    if (result == null)
                    {
                        return new List<Shift>();
                    }

                    return await result;
                }
            
        }
    }
}