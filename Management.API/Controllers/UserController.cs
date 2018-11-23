using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Dapper;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands;
using Management.Domain.Queries;
using Management.API.Helpers;
using Management.Domain.DomainElements.BudgetPlanner;
using Management.Domain.Queries.Shift;
using Management.Domain.Queries.User;
using Management.Domain.Queries.WorkSchedule;
using Management.Infrastructure.MessagingContracts;
using Management.Persistence.Helpers;
using Management.Persistence.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Management.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public partial class UserController : BaseController
    {
        private readonly IOptions<IdentityServerConfiguration> _identityConfig;

        public UserController(ICommandRouter commandRouter, IQueryRouter queryRouter, IOptions<IdentityServerConfiguration> identityConfig) : base(commandRouter, queryRouter)
        {
            _identityConfig = identityConfig;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetALLUsers()
        {
            var result = await QueryRouter.QueryAsync<GetUsers, IEnumerable<User>>(new GetUsers());

            return new ObjectResult(result);

        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await QueryRouter.QueryAsync<GetUserById, User>(new GetUserById(id));

            return new ObjectResult(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var result = await CommandRouter.RouteAsync<DeleteUserByIdCommand, IdResponse>(new DeleteUserByIdCommand(id));
            
            return new ObjectResult(result);
        }

        
        
        [HttpPost]
        [Route("{id}/shifts/{shiftId}")]
        public async Task<IActionResult> AssignToShift(Guid id, Guid shiftId)
        {
            var result = await CommandRouter.RouteAsync<AssignUserToShiftCommand, IdResponse>(
                new AssignUserToShiftCommand(id, shiftId));
            
            return new ObjectResult(result);
        }

        [HttpGet]
        [Route("{id}/shifts")]
        public async Task<IActionResult> GetShiftsForUserWithIdAsync(Guid id)
        {
            var result = await QueryRouter.QueryAsync<GetShiftsForUserWithId, IEnumerable<Shift>>(new GetShiftsForUserWithId(id));

            
            
            return new ObjectResult(result);
        }

        [HttpGet]
        [Route("wage/{id}")]
        public async Task<IActionResult> GetWageForUserWithIdAsync(Guid id)
        {
            var result = await QueryRouter.QueryAsync<GetWageForUserWithId, ShiftPayment>(new GetWageForUserWithId(id));
 
            return new ObjectResult(result);
        }
        
        [HttpGet]
        [Route("hours/{id}")]
        public async Task<IActionResult> GetWorkHoursForUserWithIdAsync(Guid id)
        {
            var shifts = await QueryRouter.QueryAsync<GetWorkHoursForUser, ShiftPayment>(new GetWorkHoursForUser(id));
 
            return new ObjectResult(shifts);
        }

        
        [HttpGet]
        [Route("salary/{id}")]
        public async Task<IActionResult> GetSalaryForUserWithIdAsync(Guid id)
        {
            var result = await QueryRouter.QueryAsync<GetSalaryForUserWithId, IEnumerable<ShiftPayment>>(new GetSalaryForUserWithId(id));
            
            
            return new ObjectResult(result);
        }
        
       
        
        
        
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserById(Guid id , [FromBody]  UpdateUserRequestModel userRequestModel)
        {
            //Calls update controller on Identity API, so the db is synced
            var identityBaseurl = _identityConfig.Value.IdentityServerUrl + "/identity/users/" + id;
            var httpClient = new HttpClient();
            
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var jsonUpdateStatement = JsonConvert.SerializeObject(new UpdateIdentityUser
            {
                Email = userRequestModel.Email,
                Password = userRequestModel.Password,
                AuthLevel = userRequestModel.Accesslevel
            }, Formatting.Indented);
            
            var httpContent = new StringContent(jsonUpdateStatement, System.Text.Encoding.UTF8, "application/json");
            var identityResult = await httpClient.PutAsync(identityBaseurl, httpContent);
            

            if (!identityResult.IsSuccessStatusCode)
            {
                return StatusCode(400, "Identity server could not be reached.");
            }
            
            //Update API db (seperate from identity db)
            var result = await CommandRouter.RouteAsync<UpdateUserCommand, IdResponse>(
                new UpdateUserCommand(userRequestModel.Name, userRequestModel.Email, userRequestModel.Password,
                    userRequestModel.Accesslevel , id));
            
            return new ObjectResult(result);
        }
    }
}