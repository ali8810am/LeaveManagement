using LeaveManagement.Application.DTOs.LeaveRequest;
using LeaveManagement.Application.Dtos.LeaveType;
using LeaveManagement.Application.DTOs.LeaveType;
using LeaveManagement.Application.Features.LeaveTypes.Request.Commands;
using LeaveManagement.Application.Features.LeaveTypes.Request.Queries;
using LeaveManagement.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LeaveManagement.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrator")]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<HomeController>
        [HttpGet]
    
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveTypes =await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> Get(int id)
        {
            var leaveType =await _mediator.Send(new GetLeaveTypeDetailsRequest { Id = id });
            return Ok(leaveType);
        }

        // POST api/<HomeController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveTypeDto type)
        {
           var response= await _mediator.Send(new CreateLeaveTypeCommand { LeaveTypeDto = type });
           return Ok(response);
        }

        // PUT api/<HomeController>/5
        [HttpPut]
        public async Task<IActionResult> Put( [FromBody] LeaveTypeDto type)
        {
            await _mediator.Send(new UpdateLeaveTypeCommand { LeaveTypeDto = type });
            return NoContent();
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var command = new DeleteLeaveTypeCommand { Id = id };
                await _mediator.Send(command);
                 return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
