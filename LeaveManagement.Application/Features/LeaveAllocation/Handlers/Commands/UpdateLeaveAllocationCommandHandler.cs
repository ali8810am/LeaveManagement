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
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {

            var validationResult = new UpdateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);


            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.Get(request.LeaveAllocation.Id);
            if (leaveAllocation != null)
            {
                await _unitOfWork.LeaveAllocationRepository.Update(leaveAllocation);
                await _unitOfWork.Save();
            }
            return Unit.Value;
        }
    }
}
