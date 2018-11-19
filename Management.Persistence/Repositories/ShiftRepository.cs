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
        Task<IEnumerable<Shift>> GetForUserWithIdAsync(Guid Id);
        Task<IdResponse> UpdateEmployeeOnShift(Guid employeeId, Guid ShiftID);
        Task<IEnumerable<Shift>> GetSalaryForUserAsync(Guid id);
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
            
             
                using (var conn = new NpgsqlConnection(ConnectionString.GetConnectionString()))
                {
                    conn.Open();


                    
                    
                    //var result = conn.QueryAsync<Shift>("Select * from shifts where Id in --> (Select shiftId from hasShift where employeeId = @Id " , new {Id = Guid.NewGuid()}); //TODO : <-- Discuss SQL injection attack

                    //https://stackoverflow.com/questions/13653461/dapper-and-sql-injections/13653484
                    // https://github.com/StackExchange/Dapper
                    
                    var result = conn.QueryAsync<Shift>("Select id from shifts where id = @Id ",
                        new {Id = Guid.Parse("29bedaff-6a04-4466-bb27-7132f9f4c4ab") , });

                    
                    
                    
                        
                    
                    
                   if (result == null)
                    {
                        return new List<Shift>();
                    }

                    return await result;
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

        public async Task<IEnumerable<Shift>>GetSalaryForUserAsync(Guid id)
        {
            using (var conn= new NpgsqlConnection(ConnectionString.GetConnectionString()))
            {
                conn.Open();
                
                var result = conn.QueryAsync<Shift>("Select SUM (duration) from shifts where Id in (Select shiftId from hasShift where employeeId='"+ id +"');");

                return await result;
            }
            
            
        }
    }
}
