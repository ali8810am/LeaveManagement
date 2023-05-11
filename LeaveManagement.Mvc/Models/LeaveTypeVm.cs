using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Mvc.Models
{
    public class LeaveTypeVm:CreateLeaveTypeVm
    {
        public int Id { get; set; }
    }

    public class CreateLeaveTypeVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Default numbers of days")]
        public int DefaultDays { get; set; }



    }
}
