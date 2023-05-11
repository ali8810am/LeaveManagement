using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Domain;

namespace LeaveManagement.Application.Contracts.Presistence
{
    public interface ILeaveTypeRepository:IGenericRepository<LeaveType>
    {
    }
}
