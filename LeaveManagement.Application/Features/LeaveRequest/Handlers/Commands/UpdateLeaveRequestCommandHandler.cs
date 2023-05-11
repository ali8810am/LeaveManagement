using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;

using MediatR;
using ValidationException = LeaveManagement.Application.Exceptions.ValidationException;

namespace LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler:IRequestHandler<UpdateLeaveRequestCommand,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest =await _unitOfWork.LeaveRequestRepository.Get(request.Id);
            if (leaveRequest == null) 
                throw new NotFoundException(nameof(leaveRequest),request.Id);
            if (request.LeaveRequest!=null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.LeaveRequest);
                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult);
                _mapper.Map(request.LeaveRequest, leaveRequest);
                await _unitOfWork.LeaveRequestRepository.Update(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest,
                    request.ChangeLeaveRequestApprovalDto.Approved);
                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocation =await _unitOfWork.LeaveAllocationRepository.GetUserAllocation(leaveRequest.RequestngEmployeeId,
                        leaveRequest.LeaveTypeId);
                    var daysRequested=(int)(leaveRequest.EndDate-leaveRequest.StartDate).TotalDays;
                    allocation.NumberOfDays -= daysRequested;
                    await _unitOfWork.LeaveAllocationRepository.Update(allocation);
                }
            }
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
