using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands;
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
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel requestModel)
        {
            var baseurl = _identityConfig.Value.IdentityServerUrl + "/identity/users";

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            
            //Serialize object to json-format
            var json = JsonConvert.SerializeObject(requestModel, Formatting.Indented);
            
            //Httpcontent
            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            
            var identityResult = await httpClient.PostAsync(baseurl, httpContent);

            if (!identityResult.IsSuccessStatusCode)

            {
                if (identityResult.Content != null)
                {
                    var errorMsg = await identityResult.Content.ReadAsStringAsync();
                    return StatusCode((int)identityResult.StatusCode , errorMsg);
                }
                
                return StatusCode((int)identityResult.StatusCode, identityResult.ReasonPhrase);
            }
            
            

            var response = await CommandRouter.RouteAsync<CreateUserCommand, IdResponse>(
                new CreateUserCommand(requestModel.Name, requestModel.Email, requestModel.Password , requestModel.Acceslevel));
            
            if (!response.IsSuccessful)
            {
                return StatusCode(400, response.Message);
            }
            
            return new ObjectResult(response.Id);
        }
    }
}