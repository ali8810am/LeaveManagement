using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands
{
    public class UpdateLeaveAllocationCommand: IRequest<Unit>
    {
        public LeaveAllocationDto LeaveAllocation { get; set; }
    }
}
