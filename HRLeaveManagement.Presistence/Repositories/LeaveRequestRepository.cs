using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Presistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private ApplicationDbContext _context;

        public LeaveRequestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus)
        {
            leaveRequest.Approved = ApprovalStatus;
            _context.Entry(leaveRequest).State =EntityState.Modified;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            return await _context.LeaveRequests
                 .Include(r => r.LeaveType)
                 .ToListAsync();
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequest= await _context.LeaveRequests
                .Include(r => r.LeaveType)
                .FirstOrDefaultAsync(r => r.Id == id);
            return leaveRequest;

        }

        public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails(string userId)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(r => r.LeaveType)
                .Where(r => r.RequestngEmployeeId == userId).ToListAsync();
            return leaveRequest;
        }
    }
}
