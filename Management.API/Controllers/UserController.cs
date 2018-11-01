using System.Collections.Generic;
using System.Threading.Tasks;
using Management.Domain.DomainModels.Users;
using Management.Domain.Queries;
using Management.Infrastructure.MessagingContracts;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public partial class UserController : BaseController
    {
        public UserController(ICommandRouter commandRouter, IQueryRouter queryRouter) : base(commandRouter, queryRouter)
        {
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllUsers(int page, int pageSize)
        {
            var result = await QueryRouter.QueryAsync<GetUsers, IEnumerable<User>>(new GetUsers(page, pageSize));

            if (result == null)
            {
                return NotFound();
            }
            return new ObjectResult(result);
            
        }
        
        
            
            
            
        
       
    }
}