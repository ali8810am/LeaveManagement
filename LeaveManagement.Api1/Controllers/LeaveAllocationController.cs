using LeaveManagement.Application.DTOs.LeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetLeaveAllocationListRequest();
            var leaveAllocations = await _mediator.Send(request);
            return Ok(leaveAllocations);
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var request=new GetLeaveAllocationDetailsRequest{Id = id};
            var leaveAllocation=await _mediator.Send(request);
            return Ok(leaveAllocation);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveAllocationDto value)
        {
            var response=await _mediator.Send(new CreateLeaveAllocationCommand { LeaveAllocationDto = value });
            return Ok(response);
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut]
        public async Task<IActionResult> Put( [FromBody] LeaveAllocationDto value)
        {
            var command = new UpdateLeaveAllocationCommand {LeaveAllocation = value};
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
