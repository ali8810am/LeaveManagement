using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Constants;
using LeaveManagement.Application.Contracts.Presistence;
using Microsoft.AspNetCore.Http;

namespace LeaveManagement.Presistence.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ILeaveTypeRepository _leaveTypeRepository;
        private ILeaveAllocationRepository _leaveAllocationRepository;
        private IHttpContextAccessor _contextAccessor;
        private ILeaveRequestRepository _leaveRequestRepository;

        public UnitOfWork(ApplicationDbContext context,IHttpContextAccessor contextAccessor)
        {
            _context = context;
                this._contextAccessor = contextAccessor;
        }

        public void Dispose()
        {
           _context.Dispose();
           GC.SuppressFinalize(this);
        }

        public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);
        public ILeaveAllocationRepository LeaveAllocationRepository =>_leaveAllocationRepository??=new LeaveAllocationRepository(_context);
        public ILeaveRequestRepository LeaveRequestRepository =>_leaveRequestRepository??=new LeaveRequestRepository(_context);
        public async Task Save()
        {
            var username = _contextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.UId)?.Value;
            await _context.SaveChangesAsync(username);
        }
    }
}
