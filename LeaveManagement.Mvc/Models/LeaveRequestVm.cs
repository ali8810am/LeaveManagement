using System.ComponentModel.DataAnnotations;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Domain;
using LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagement.Mvc.Models
{
    public class LeaveRequestVm:CreateLeaveRequestVm
    {
        public int Id { get; set; }
        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }

        [Display(Name = "Date Actioned")]
        public DateTime DateActiond { get; set; }

        [Display(Name = "Approval State")]
        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }
        public LeaveTypeVm LeaveType { get; set; }
        public EmployeeVm Employee { get; set; }

    }
    public class CreateLeaveRequestVm
    {
        [Required]
        [Display(Name = "StartDate")]
        public string StartDate { get; set; }
        [Required]
        [Display(Name = "EndDate")]
        public string EndDate { get; set; }
        [MaxLength(300)]
        [Display(Name = "Comments")]
        public string RequestComments { get; set; }
        [Required]
        [Display(Name = "Leave type")]
        public int LeaveTypeId { get; set; }
        public SelectList? LeaveTypes { get; set; }


    }

    public class AdminLeaveRequestViewVm
    {
        [Display(Name = "Total Number Of Requests")]
        public int TotalRequests { get; set; }
        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }
        [Display(Name = "Rejected Requests")]
        public int RejectedRequests { get; set; }
        public List<LeaveRequestVm> LeaveRequests { get; set; }
    }
    public class EmployeeLeaveRequestViewVm
    {
        public List<LeaveAllocationVm> LeaveAllocations { get; set; }
        public List<LeaveRequestVm> LeaveRequests { get; set; }
    }
}
