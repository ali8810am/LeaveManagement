using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Dtos.LeaveAllocation;
using LeaveManagement.Application.DTOs.LeaveAllocation;
using LeaveManagement.Application.Dtos.LeaveType;
using LeaveManagement.Application.Responses;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands
{
    public class CreateLeaveAllocationCommand:IRequest<BaseCommandResponse>
    {
        public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
