using System.Threading.Tasks;
using Management.API.RequestModels.OrganizationRequestModel;
using Management.Documents.Documents;
using Management.Domain.Commands.OrganizationCommands;
using Management.Infrastructure.MessagingContracts;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationController : BaseController
    {
        public OrganizationController(ICommandRouter commandRouter, IQueryRouter queryRouter) : base(commandRouter, queryRouter)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequestModel requestModel)
        {
            var respons = await CommandRouter.RouteAsync<CreateOrganiztion, IdResponse>(new CreateOrganiztion(requestModel.Name, requestModel.Address));
            return null;
        }
        
        
        
    }
}