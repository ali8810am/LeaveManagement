using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.Mvc.Models
{
    public class RegisterVm
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get;set; }
        [Required]
        [EmailAddress]
        public string Email { get;set; }
        [Required]
        [MaxLength(100)]
        [MinLength(6)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get;set; }
    }
}
