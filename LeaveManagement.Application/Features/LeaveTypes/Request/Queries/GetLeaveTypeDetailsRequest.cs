using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Dtos.LeaveAllocation;
using LeaveManagement.Application.Dtos.LeaveType;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveTypes.Request.Queries
{
    public class GetLeaveTypeDetailsRequest : IRequest<LeaveTypeDto>
    {
        public int Id { get; set; }
    }
}
