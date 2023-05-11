using LeaveManagement.Mvc.Models;

namespace LeaveManagement.Mvc.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string username, string password);
        Task<bool> Register(RegisterVm register);
        Task Logout();
    }
}
