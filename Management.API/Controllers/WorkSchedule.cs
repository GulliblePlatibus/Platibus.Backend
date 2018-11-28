using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Management.Domain.Queries.WorkSchedule;
using Management.Infrastructure.MessagingContracts;
using Management.Persistence.Model;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Route("api/workschedule")]
    [ApiController]
    public class WorkSchedule : BaseController
    {

        
        public WorkSchedule(IQueryRouter queryRouter, ICommandRouter commandRouter) : base(commandRouter, queryRouter)
        {
            
            
            
        }

        [HttpGet]

        public async Task<IActionResult> GetWorkSchedule()
        {
            var result = await
                QueryRouter.QueryAsync<AllShiftsAndEmployeesQuery, List<AllShiftsWithEmployees>>(
                    new AllShiftsAndEmployeesQuery());
            
            return new ObjectResult(result);
        }

        [HttpGet]
        [Route("UserShiftDetailed")]
        public async Task<IActionResult> GetUserShiftDetailed()
        {
            var result = await
                QueryRouter.QueryAsync<UserShiftDetailedQuery, List<UserShiftDetailed>>(new UserShiftDetailedQuery());
            
            return new ObjectResult(result);
        }
    }
    
}