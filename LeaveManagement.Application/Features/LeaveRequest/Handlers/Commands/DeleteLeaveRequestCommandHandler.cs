using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;

using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    internal class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveRequestCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.Get(request.Id);
            if (leaveRequest != null)
            {
                await _unitOfWork.LeaveRequestRepository.Delete(leaveRequest);
                await _unitOfWork.Save();
            }
            return Unit.Value;
        }
    }
}
