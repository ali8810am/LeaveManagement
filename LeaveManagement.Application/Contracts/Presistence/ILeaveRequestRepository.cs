using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Domain;

namespace LeaveManagement.Application.Contracts.Presistence
{
    public interface ILeaveRequestRepository:IGenericRepository<LeaveRequest>
    {
        Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus);
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
        Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
        Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId);

    }
}
