using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.DTOs.LeaveRequest;
using LeaveManagement.Application.Responses;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
    public class CreateLeaveRequestCommand:IRequest<BaseCommandResponse>
    {
        public CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}
