using System;
using System.Threading.Tasks;
using Management.API.RequestModels;
using Management.Infrastructure.MessagingContracts;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateSupplement([FromBody] CreateSupplementRequestModel requestModel)
        {
            return null;

        }
        
        
    }
    
   
    
    
    
}