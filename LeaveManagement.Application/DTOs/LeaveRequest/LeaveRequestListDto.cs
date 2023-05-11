using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Dtos.Common;
using LeaveManagement.Application.Dtos.LeaveType;
using LeaveManagement.Application.Models.Identity;

namespace LeaveManagement.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto:BaseDto
    {
        public Employee Employee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Approved { get; set; }
    }
}
