using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;

using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler:IRequestHandler<DeleteLeaveAllocationCommand,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveAllocationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var leaveAllocation = await _unitOfWork.LeaveTypeRepository.Get(request.Id);
            await _unitOfWork.LeaveTypeRepository.Delete(leaveAllocation);
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
