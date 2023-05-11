using LeaveManagement.Domain;

namespace LeaveManagement.Application.Contracts.Presistence
{
    public interface ILeaveAllocationRepository:IGenericRepository<LeaveAllocation>
    {
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<bool> AllocationExist(string userId,int leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocation(string userId,int leaveTypeId);
    }
}
