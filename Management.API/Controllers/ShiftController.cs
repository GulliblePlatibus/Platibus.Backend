using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.API.RequestModels;
using Management.Documents.Documents;
using Management.Domain.Commands;
using Management.Domain.Commands.ShiftCommands;
using Management.Domain.Queries;
using Management.Domain.Queries.Shift;
using Management.Infrastructure.MessagingContracts;
using Management.Persistence.Model;
using Microsoft.AspNetCore.Mvc;
using AddManyShifts = Management.API.RequestModels.AddManyShifts;

namespace Management.API.Controllers
{
    [Route("api/shifts")]
    [ApiController]
    public class ShiftController : BaseController
    {
        public ShiftController(ICommandRouter commandRouter, IQueryRouter queryRouter) : base(commandRouter,
            queryRouter)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateShift([FromBody] CreateShiftRequestModel requestModel)
        {

            Console.WriteLine();
            var response = await CommandRouter.RouteAsync<CreateShiftCommand, IdResponse>(
                new CreateShiftCommand(requestModel.ShiftStart, requestModel.ShiftEnd , requestModel.EmployeeId));

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
            var result = await QueryRouter.QueryAsync<GetShiftById, Shift>(new GetShiftById(id));

            return new ObjectResult(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteShiftById(Guid id)
        {
            var result =
                await CommandRouter.RouteAsync<DeleteShiftByIdCommand, IdResponse>(new DeleteShiftByIdCommand(id));

            return new ObjectResult(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateShiftById( [FromBody] UpdateShiftRequestModel shiftRequestModel)
        {
            var result = await CommandRouter.RouteAsync<UpdateShiftCommand, IdResponse>(
                new UpdateShiftCommand(shiftRequestModel.ShiftId, shiftRequestModel.ShiftStart, shiftRequestModel.ShiftEnd , shiftRequestModel.EmployeeId));

            return new ObjectResult(result);
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUserToShift([FromBody] AddUserToShiftRequestModel addUserToShiftRequestModel)
        {
            var result = await CommandRouter.RouteAsync<AssignUserToShiftCommand, IdResponse>(
                new AssignUserToShiftCommand(addUserToShiftRequestModel.EmployeeOnShift,
                    addUserToShiftRequestModel.id));
           
            return new ObjectResult(result);
        }
        
        [HttpPost]
        [Route("AddManyShifts")]
        public async Task<IActionResult> AddManyShifts([FromBody] AddManyShifts addUserToShiftRequestModel)
        {
            var result = await CommandRouter.RouteAsync<AddManyShiftsCommand, IdResponse>(
                new AddManyShiftsCommand(addUserToShiftRequestModel.listOfShifts));
            
            

            return new ObjectResult(result);
        }

        [HttpPost]
        [Route("UpdateShiftAndEmployee")]
        public async Task<IActionResult> UpdateShiftAndEmployee(
            [FromBody] UpdateShiftAndEmployeeRequestModel updateShiftAndEmployeeRequestModel)
        {

            var result = await CommandRouter.RouteAsync<CreateShiftWithEmployeeCommand, IdResponse>(
                new CreateShiftWithEmployeeCommand(updateShiftAndEmployeeRequestModel.ShiftStart,
                    updateShiftAndEmployeeRequestModel.ShiftEnd , updateShiftAndEmployeeRequestModel.EmployeeId));

            return new ObjectResult(result);
        }
    }

}