using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Dtos.LeaveType;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveTypes.Request.Commands
{
    public class UpdateLeaveTypeCommand: IRequest<Unit>
    {
    
        public LeaveTypeDto LeaveTypeDto { get; set; }
    }
}
