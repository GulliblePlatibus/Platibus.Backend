using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands.ShiftCommands;
using Management.Domain.Commands.SupplementCommands;
using Management.Infrastructure.MessagingContracts;
using Microsoft.AspNetCore.Mvc;
using HourInfo = Management.Domain.DomainElements.BudgetPlanner.ValueObjects.HourInfo;

namespace Management.API.Controllers
{
    [Route("api/supplement")]
    [ApiController]
    public class SupplementController : BaseController
    {
        public SupplementController(ICommandRouter commandRouter, IQueryRouter queryRouter) : base(commandRouter, queryRouter)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateSupplement([FromBody]CreateSupplementRequestModel requestModel)
        {
            var list = new List<HourInfo>();

            foreach (var hourInfoRequestModel in requestModel.TimeRange)
            {
                list.Add(new HourInfo(hourInfoRequestModel.FromHour, hourInfoRequestModel.ToHour));
            }
            
            var response = await CommandRouter.RouteAsync<CreateSupplementCommand, IdResponse>(
                new CreateSupplementCommand(requestModel.Name, requestModel.Decription, requestModel.IsStaticSupplement, requestModel.SupplementValue, requestModel.SupplementDays, list));

            if (!response.IsSuccessful)
            {
                return StatusCode(401, response.Message);
            }

            return new ObjectResult(response.Id);
        }
        
        

    }
        
        
}
    
   
    
    
    
