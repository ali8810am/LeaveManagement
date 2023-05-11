﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveTypes.Request.Commands
{
    public class DeleteLeaveTypeCommand: IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
