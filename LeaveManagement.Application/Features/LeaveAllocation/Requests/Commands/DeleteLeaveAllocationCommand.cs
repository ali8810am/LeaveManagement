﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands
{
    public class DeleteLeaveAllocationCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
