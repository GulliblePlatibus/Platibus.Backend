using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dapper;
using Dommel;
using Management.Documents.Documents;
using Management.Persistence.Model;
using Npgsql;

namespace Management.Persistence.Repositories
{


    public interface IWorkScheduleRepository : IBaseRepository<WorkSchedule> 
    {
        Task<IEnumerable<UserShiftDetailed>> GetUserShiftDetailed();

    }

    public class WorkScheduleRepository : BaseRepository<WorkSchedule>, IWorkScheduleRepository
    {
        private readonly IConnectionString _connectionString;

        public WorkScheduleRepository(IConnectionString connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<IEnumerable<UserShiftDetailed>> GetUserShiftDetailed()
        {
            using (var conn = new NpgsqlConnection(_connectionString.GetConnectionString()))
            {
                var result =  conn.QueryAsync<Model.UserShiftDetailed>( 
                    "select users.name , shifts.shiftstart , shifts.shiftend , users.id from hasshift , shifts , users Where (hasshift.shiftid = shifts.id and hasshift.employeeid = users.id)").Result.ToList();



                return result;

            }

            
        
        
           
        }

        
    }
}