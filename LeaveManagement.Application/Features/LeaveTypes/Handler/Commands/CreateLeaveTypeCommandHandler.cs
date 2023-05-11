using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.Dtos.LeaveType;
using LeaveManagement.Application.DTOs.LeaveType.Validators;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Features.LeaveTypes.Request.Commands;
using LeaveManagement.Application.Responses;
using LeaveManagement.Domain;
using MediatR;
using ValidationException = LeaveManagement.Application.Exceptions.ValidationException;

namespace LeaveManagement.Application.Features.LeaveTypes.Handler.Commands
{
    public class CreateLeaveTypeCommandHandler:IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveTypeDtoValidators();
            var validateResult = await validator.ValidateAsync(request.LeaveTypeDto, cancellationToken);
            if (!validateResult.IsValid)
            {
                response.Message = "Creation failed";
               response.Success = false;
               response.Errors = validateResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {


                var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
                leaveType = await _unitOfWork.LeaveTypeRepository.Add(leaveType);
                response.Success = true;
                response.Message = "successful";
                response.Id = leaveType.Id;

            }
            await _unitOfWork.Save();
            return response;
        }
    }
}
