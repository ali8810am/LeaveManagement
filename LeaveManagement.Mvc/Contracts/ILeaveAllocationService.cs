using LeaveManagement.Mvc.Services.Base;

namespace LeaveManagement.Mvc.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}
