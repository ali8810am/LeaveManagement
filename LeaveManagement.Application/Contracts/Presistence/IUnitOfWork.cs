using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Application.Contracts.Presistence
{
    public interface IUnitOfWork:IDisposable
    {
        ILeaveTypeRepository LeaveTypeRepository { get; }
        ILeaveAllocationRepository LeaveAllocationRepository { get; }
        ILeaveRequestRepository LeaveRequestRepository { get; }
        Task Save();
    }
}
