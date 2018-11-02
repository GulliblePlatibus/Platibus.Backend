using System.Threading.Tasks;
using Management.API.Helpers;
using Management.Infrastructure.MessagingContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Management.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public partial class UserController : BaseController
    {
        private readonly IOptions<IdentityServerConfiguration> _identityConfig;

        public UserController(ICommandRouter commandRouter, IQueryRouter queryRouter, IOptions<IdentityServerConfiguration> identityConfig) : base(commandRouter, queryRouter)
        {
            _identityConfig = identityConfig;
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetALLUsers()
        {
            return null;
        }
       
    }
}