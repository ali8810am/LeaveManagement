using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.DTOs.LeaveType.Validators;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveTypes.Request.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Application.Features.LeaveTypes.Handler.Commands
{
    public class UpdateLeaveTypeCommandHandler:IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.LeaveTypeDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);
            var leaveType =await _unitOfWork.LeaveTypeRepository.Get(request.LeaveTypeDto.Id);
            if (leaveType == null) throw new NotFoundException(request.LeaveTypeDto.Name, request.LeaveTypeDto);
            _mapper.Map(request.LeaveTypeDto, leaveType);
            await _unitOfWork.LeaveTypeRepository.Update(leaveType);
            await _unitOfWork.Save();
            return Unit.Value;
        }
    }
}
