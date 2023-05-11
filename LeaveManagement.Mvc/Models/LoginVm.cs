using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Mvc.Models
{
    public class LoginVm
    {
        [Required]
        //[EmailAddress]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
