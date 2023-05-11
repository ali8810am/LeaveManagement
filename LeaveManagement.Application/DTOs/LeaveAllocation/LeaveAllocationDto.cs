using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Dtos.Common;
using LeaveManagement.Application.Dtos.LeaveAllocation;
using LeaveManagement.Application.Dtos.LeaveType;
using LeaveManagement.Application.Models.Identity;

namespace LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class LeaveAllocationDto:BaseDto
    {
        public int NumberOfDays { get; set; }
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
