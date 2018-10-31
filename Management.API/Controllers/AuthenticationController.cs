using System.Linq;
using System.Threading.Tasks;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands;
using Management.Domain.Queries;
using Management.Infrastructure.MessagingContracts;
using Management.Persistence.Model;
using Management.Queries.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(ICommandRouter commandRouter, IQueryRouter queryRouter) : base(commandRouter, queryRouter)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel requestModel)
        {
            var result = await CommandRouter.RouteAsync<CreateUserCommand, IdResponse>(
                new CreateUserCommand(requestModel.Name, requestModel.Email, requestModel.Password));

            if (!result.IsSuccessful)
            {
                return StatusCode(400, result.Message);
            }
            
            return new ObjectResult(result.Id);
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await QueryRouter.QueryAsync<LoginQuery, User>(new LoginQuery(email, password));

            if (result == null)
            {
                return NotFound();
            }
            
            return new ObjectResult(result);
        }

        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<IActionResult> Authorize()
        {
            return new JsonResult(from c in User.Claims select new {c.Type , c.Value});
        }

        [HttpGet]
        [Route("DoAwesomeStuuf")]
        public async Task<IActionResult> DoStuff()
        {
            var result = await QueryRouter.QueryAsync<GetUsers, User>(new GetUsers(0, 10));

            if (result == null)
            {
                return NotFound();
            }
            
            return new ObjectResult(result);
        }
        
    }
}