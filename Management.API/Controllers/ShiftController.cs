using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands.ShiftCommands;
using Management.Domain.Queries;
using Management.Domain.Queries.Shift;
using Management.Infrastructure.MessagingContracts;
using Management.Persistence.Model;
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
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetALLShifts()
        {
            var result = await QueryRouter.QueryAsync<GetAllShifts, IEnumerable<Shift>>(new GetAllShifts());

            return new ObjectResult(result);

        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await QueryRouter.QueryAsync<GetShiftById, User>(new GetShiftById(id));

            return new ObjectResult(result);
        }
    }
    
    
   
}