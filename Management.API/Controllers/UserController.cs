using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands;
using Management.Domain.Queries;
using Management.Infrastructure.MessagingContracts;
using Management.Persistence.Model;
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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserById(Guid id , [FromBody]  UpdateUserRequestModel userRequestModel)
        {
            var result = await CommandRouter.RouteAsync<UpdateUserCommand, IdResponse>(
                new UpdateUserCommand(userRequestModel.Name, userRequestModel.Email, userRequestModel.Password,
                    userRequestModel.Accesslevel , id));
            
            return new ObjectResult(result);
        }
        
       
    }
}