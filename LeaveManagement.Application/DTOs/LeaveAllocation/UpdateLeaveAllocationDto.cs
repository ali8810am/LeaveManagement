using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Dtos.Common;
using LeaveManagement.Application.Dtos.LeaveAllocation;
using LeaveManagement.Application.Dtos.LeaveType;

namespace LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class UpdateLeaveAllocationDto:BaseDto,ILeaveAllocationDto
    {
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
