using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Dommel;
using Management.Persistence.Model;
using Management.Persistence.Model.Budget;
using Npgsql;

namespace Management.Persistence.Repositories
{
    public interface ISalaryRepository : IBaseRepository<Salary>
    {
        Task<Salary> GetWageForUserWithIdAsync(Guid Id);
        Task<Salary> GetWorkHoursForUserAsync(Guid id);
    }

    public class SalaryRepository : BaseRepository<Salary>, ISalaryRepository
    {
        private readonly IConnectionString ConnectionString;
        
        public SalaryRepository(IConnectionString _connectionString) : base(_connectionString)
        {
            ConnectionString = _connectionString;
            
        }

        public async Task<Salary>GetWorkHoursForUserAsync(Guid id)
        {
            using (var conn= new NpgsqlConnection(ConnectionString.GetConnectionString()))
            {
                conn.Open();
                
                var result = await conn.QueryFirstAsync<Salary>("SELECT SUM (duration) FROM shifts WHERE Id IN (SELECT shiftId FROM hasShift WHERE employeeId='"+ id +"');");

                return result;
            }
               
        }
        public async Task<Salary> GetWageForUserWithIdAsync(Guid Id)
        {
            using (var conn = new NpgsqlConnection(ConnectionString.GetConnectionString()))
            {
                conn.Open();

                var result = await conn.QueryFirstAsync<Salary>("SELECT wage FROM users WHERE id ='" + Id + "'");

                return result;
            }
        }
    }
}