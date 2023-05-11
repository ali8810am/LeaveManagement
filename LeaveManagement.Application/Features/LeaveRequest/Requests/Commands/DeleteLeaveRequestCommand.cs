using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Requests.Commands
{
    public class DeleteLeaveRequestCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
