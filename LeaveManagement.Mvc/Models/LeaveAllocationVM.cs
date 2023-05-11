using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagement.Mvc.Models;

namespace LeaveManagement.MVC.Models
{
    public class LeaveAllocationVm
    {
        public int Id { get; set; }
        [Display(Name = "Number Of Days")]

        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }
        
        public EmployeeVm EmployeeVm { get; set; }
        public int LeaveTypeId { get; set; }
    }

    public class CreateLeaveAllocationVm
    {
        public int LeaveTypeId { get; set; }
    }

    public class UpdateLeaveAllocationVm
    {
        public int Id { get; set; }

        [Display(Name="Number Of Days")]
        [Range(1,50, ErrorMessage = "Enter Valid Number")]
        public int NumberOfDays { get; set; }
        public LeaveTypeVm LeaveType { get; set; }

    }

    public class ViewLeaveAllocationsVm
    {
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVm> LeaveAllocations { get; set; }
    }
}
