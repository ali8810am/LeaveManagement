using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Domain;

namespace LeaveManagement.Presistence.Repositories
{
    public class LeaveTypeRepository:GenericRepository<LeaveType>,ILeaveTypeRepository
    {
        private ApplicationDbContext _context;

        public LeaveTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
