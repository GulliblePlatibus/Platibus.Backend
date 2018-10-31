using System.Threading.Tasks;
using Management.Infrastructure.MessagingContracts;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public partial class UserController : BaseController
    {
        public UserController(ICommandRouter commandRouter, IQueryRouter queryRouter) : base(commandRouter, queryRouter)
        {
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetALLUsers()
        {
            
        }
       
    }
}