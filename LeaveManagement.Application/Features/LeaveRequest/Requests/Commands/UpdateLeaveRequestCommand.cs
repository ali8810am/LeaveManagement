using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
    public class UpdateLeaveRequestCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateLeaveRequestDto LeaveRequest { get; set; }
        public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; }

    }
}
