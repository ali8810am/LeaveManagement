using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveTypes.Request.Commands;

using MediatR;

namespace LeaveManagement.Application.Features.LeaveTypes.Handler.Commands
{
    public class DeleteLeaveTypeCommandHandler:IRequestHandler<DeleteLeaveTypeCommand,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveType =await _unitOfWork.LeaveTypeRepository.Get(request.Id);
            if (leaveType == null)
                throw new NotFoundException(nameof(leaveType), request.Id);
            await _unitOfWork.LeaveTypeRepository.Delete(leaveType);
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
