using LeaveManagement.Mvc.Models;
using LeaveManagement.Mvc.Services.Base;

namespace LeaveManagement.Mvc.Contracts
{
    public interface ILeaveRequestService
    {
        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVm leaveRequest);
        Task<AdminLeaveRequestViewVm> GetAdminLeaveRequestList();
        Task<EmployeeLeaveRequestViewVm> GetUserLeaveRequests();
        Task<LeaveRequestVm> GetLeaveRequest(int id);
        Task DeleteLeaveRequest(int id);
        Task ApproveLeaveRequest(int id, bool approved);


    }
}
