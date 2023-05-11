using LeaveManagement.Mvc.Models;
using LeaveManagement.Mvc.Services.Base;

namespace LeaveManagement.Mvc.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVm>> GetLeaveTypes();
        Task<LeaveTypeVm> GetLeaveType(int id);
        Task<Response<int>> CreateLeaveType(CreateLeaveTypeVm leaveType);
        Task<Response<int>> UpdateLeaveType(LeaveTypeVm leaveType);
        Task<Response<int>> DeleteLeaveType(int id);

    }
}
