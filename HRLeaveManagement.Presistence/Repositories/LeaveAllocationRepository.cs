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
    public class LeaveAllocationRepository:GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private ApplicationDbContext _context;

        public LeaveAllocationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            return await _context.LeaveAllocations
                .Include(a => a.LeaveType)
                .ToListAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            return await _context.LeaveAllocations
                .Where(al=>al.EmployeeId==userId)
                .Include(a => a.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await _context.LeaveAllocations.FindAsync(id);
        }

        public async Task<bool> AllocationExist(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations
                .AnyAsync(a => a.EmployeeId == userId && a.LeaveTypeId == leaveTypeId && a.Period == period);
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
           await _context.AddRangeAsync(allocations);
        }

        public async Task<LeaveAllocation> GetUserAllocation(string userId, int leaveTypeId)
        {
            var allocation =
                await _context.LeaveAllocations.FirstOrDefaultAsync(al =>
                    al.LeaveTypeId == leaveTypeId && al.EmployeeId == userId);
            return allocation;
        }
    }
}
