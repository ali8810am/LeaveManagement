
using LeaveManagement.Application.DTOs.LeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using LeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveRequestController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser=false)
        {
            var request = new LeaveRequestListRequest{IsLoggedInUser = isLoggedInUser};
            var leaveRequests = await _mediator.Send(request);
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var request= new LeaveRequestDetailsRequest{Id = id};
            var leaveRequest = await _mediator.Send(request);
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto value)
        {
            var command = new CreateLeaveRequestCommand { LeaveRequestDto = value };
            var request=await _mediator.Send(command);
            return Ok(request);
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto value)
        {
            var command = new UpdateLeaveRequestCommand { LeaveRequest = value ,Id = id};
            var request=await _mediator.Send(command);
            return NoContent();
        }

        // PUT api/<LeaveRequestsController>/changeapproval/5
        [HttpPut("changeapproval/{id}")]
        public async Task<ActionResult> ChangeApproval(int id,[FromBody] ChangeLeaveRequestApprovalDto value)
        {
            var command = new UpdateLeaveRequestCommand { ChangeLeaveRequestApprovalDto = value, Id = id };
            var request=await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteLeaveRequestCommand { Id = id };
            var request = await _mediator.Send(command);
            return NoContent();
        }
    }
}
