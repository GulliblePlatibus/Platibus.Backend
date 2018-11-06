using System;
using System.Threading.Tasks;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands.ShiftCommands;
using Management.Infrastructure.MessagingContracts;
using Microsoft.AspNetCore.Mvc;

namespace Management.API.Controllers
{
    [Route("api/shift")]
    [ApiController]
    public class ShiftController : BaseController
    {
        public ShiftController(ICommandRouter commandRouter, IQueryRouter queryRouter) : base(commandRouter, queryRouter)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateShift([FromBody] CreateShiftRequestModel requestModel)
        {

            Console.WriteLine();
            var response = await CommandRouter.RouteAsync<CreateShiftCommand, IdResponse>(
                new CreateShiftCommand(requestModel.ShiftStart, requestModel.ShiftEnd));

            if (!response.IsSuccessful)
            {
                return StatusCode(401, response.Message);
            }
            
            return new ObjectResult(response.Id);
        }
        
    }
}