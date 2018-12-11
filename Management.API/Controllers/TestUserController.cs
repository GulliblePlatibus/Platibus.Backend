using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Management.API.Helpers;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands;
using Management.Domain.Queries;
using Management.Infrastructure.MessagingContracts;
using Management.Persistence.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2;
using Newtonsoft.Json;

namespace Management.API.Controllers
{
    public partial class UserController : BaseController
    {
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserByIdObjectAsParam(Guid id, UpdateUserRequestModel userRequestModel)
        {
            //*****************Calls update controller on Identity API, so the db is synced***************************
            //var identityBaseurl = _identityConfig.Value.IdentityServerUrl + "/identity/users/" + id;
            var identityBaseurl = "https://localhost:5001/identity/users/" + id; 
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var jsonUpdateStatement = JsonConvert.SerializeObject(new UpdateIdentityUser
            {
                Email = userRequestModel.Email,
                Password = userRequestModel.Password,
                AuthLevel = userRequestModel.AccessLevel,

            }, Formatting.Indented);

            var httpContent = new StringContent(jsonUpdateStatement, System.Text.Encoding.UTF8, "application/json");
            var identityResult = await httpClient.PutAsync(identityBaseurl, httpContent);


            if (!identityResult.IsSuccessStatusCode)
            {
                return StatusCode(400, "Identity server could not be reached.");
            }

            //*********************Update API db (seperate from identity db)************************
            //Gets user from database to handle null and empty variables in the request model
            var user = await QueryRouter.QueryAsync<GetUserById, User>(new GetUserById(id));

            //if the variables are empty or null, (or 0 for access level), they wont be updated
            if (user != null)
            {
                if (string.IsNullOrWhiteSpace(userRequestModel.Name) && !string.IsNullOrWhiteSpace(user.Name))
                    userRequestModel.Name = user.Name;
                if (string.IsNullOrWhiteSpace(userRequestModel.Email) && !string.IsNullOrWhiteSpace(user.Email))
                    userRequestModel.Email = user.Email;
                if (userRequestModel.AccessLevel == 0)
                    userRequestModel.AccessLevel = user.AccessLevel;
                if (userRequestModel.Wage == 0)
                    userRequestModel.Wage = user.BaseWage;
            }


            var result = await CommandRouter.RouteAsync<UpdateUserCommand, IdResponse>(
                new UpdateUserCommand(userRequestModel.Name, userRequestModel.Email, userRequestModel.Password,
                    userRequestModel.AccessLevel, userRequestModel.Wage, userRequestModel.EmploymentDate, id));

            return new ObjectResult(result);
        }
    }
}
