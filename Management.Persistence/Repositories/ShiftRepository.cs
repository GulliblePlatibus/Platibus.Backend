using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Management.Documents.Documents;
using Management.Persistence.Model;

using Npgsql;

namespace Management.Persistence.Repositories
{
    public interface IShiftRepository : IBaseRepository<Shift>
    {
        Task<IEnumerable<Shift>> GetForUserWithIdAsync(Guid Id, DateTime fromDate, DateTime toDate);
        Task<IdResponse> UpdateEmployeeOnShift(Guid employeeId, Guid ShiftID);
        
    }
    
    
    public class ShiftRepository : BaseRepository<Shift>, IShiftRepository
    {
        
        private readonly IConnectionString ConnectionString;
        
        public ShiftRepository(IConnectionString _connectionString) : base(_connectionString)
        {
            ConnectionString = _connectionString;
        }

        public async Task<IEnumerable<Shift>> GetForUserWithIdAsync(Guid id, DateTime fromDate, DateTime toDate)
        {
            using (var conn = new NpgsqlConnection(ConnectionString.GetConnectionString()))
            {
                conn.Open();

                var result = await conn.QueryAsync<Shift>("SELECT * FROM shifts WHERE Id IN (SELECT shiftId FROM hasShift WHERE employeeId = @Id)", new {Id = id}); //TODO : <-- Discuss SQL injection attack

                if (result == null)
                {
                    return new List<Shift>();
                }
                
                var sortOutDates = result.Where(x =>
                    x.ShiftStart.CompareTo(fromDate) > 0 && x.ShiftStart.CompareTo(toDate) < 0);
                
                //https://stackoverflow.com/questions/13653461/dapper-and-sql-injections/13653484
                // https://github.com/StackExchange/Dapper
                
                return sortOutDates;
            }
        }

        public async Task<IdResponse> UpdateEmployeeOnShift(Guid employeeId , Guid ShiftID)
        {
            using (var conn = new NpgsqlConnection(ConnectionString.GetConnectionString()))
            {
                conn.Open();
                    
                var result = conn.QueryAsync<Shift>("UPDATE shifts SET employee =  WHERE id = @ShiftId"); //TODO : <-- Discuss SQL injection attack

                if (result.IsCompleted)
                {
                    return IdResponse.Successful(employeeId);
                }
                
                return IdResponse.Unsuccessful();
            }
        }

       
    }
}
