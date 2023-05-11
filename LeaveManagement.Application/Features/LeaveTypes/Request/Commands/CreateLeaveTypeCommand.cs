using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Dtos.LeaveType;
using LeaveManagement.Application.DTOs.LeaveType;
using LeaveManagement.Application.Responses;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveTypes.Request.Commands
{
    public class CreateLeaveTypeCommand: IRequest<BaseCommandResponse>
    {
        public CreateLeaveTypeDto LeaveTypeDto { get; set; }
    }
}
