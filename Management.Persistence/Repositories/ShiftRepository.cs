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
        Task<IEnumerable<WorkSchedule>> GetForUserWithIdAsync(Guid Id);
    }
    
    
    public class ShiftRepository : BaseRepository<Shift>, IShiftRepository
    {
        
        private readonly IConnectionString _connectionString;
        
        public ShiftRepository(IConnectionString _connectionString) : base(_connectionString)
        {
            _connectionString = this._connectionString;
        }

        public async Task<IEnumerable<WorkSchedule>> GetForUserWithIdAsync(Guid id)
        {
            
                if (id.Equals(Guid.Empty))
                {
                    return null;
                }
            
                using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
                {
                    conn.Open();
                    
                    var result = await conn.QueryAsync<Shift>("Select * from shifts where Id in --> (Select shiftId from hasShift where employeeId = " + "\'" + id + "\'"); //TODO : <-- Discuss SQL injection attack
                    
                    if (result == null)
                    {
                        return null;
                    }

                    return await conn.GetAllAsync<WorkSchedule>();
                }
            
        }
    }
}