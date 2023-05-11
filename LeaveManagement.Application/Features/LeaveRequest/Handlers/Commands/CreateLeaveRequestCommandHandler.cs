using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using LeaveManagement.Application.Constants;
using LeaveManagement.Application.Contracts.Infrastructure;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using LeaveManagement.Application.Models;
using LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _contextAccessor;


        public CreateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailSender emailSender, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailSender = emailSender;
            _contextAccessor = contextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
            var userId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == CustomClaimTypes.UId)?.Value;
            var leaveAllocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocation(userId, request.LeaveRequestDto.LeaveTypeId);
            int requiredDays = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;
            if (leaveAllocation == null)
            {
                validationResult.Errors.Add(new ValidationFailure(
                    nameof(request.LeaveRequestDto.EndDate), "You dont have any allocation"));
            }
            else if (requiredDays > leaveAllocation.NumberOfDays)
            {
                validationResult.Errors.Add(new ValidationFailure(
                    nameof(request.LeaveRequestDto.EndDate), "You dont have enough days"));
            }


            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "Creation failed";
            }
            else
            {
                var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request.LeaveRequestDto);
                leaveRequest.RequestngEmployeeId = userId;
                leaveRequest = await _unitOfWork.LeaveRequestRepository.Add(leaveRequest);
                await _unitOfWork.Save();
                try
                {
                    var emailAddress = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
                    var email = new Email()
                    {
                        To = emailAddress,
                        Body =
                            $"Your leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} has been submitted successfully",
                        Subject = "Leave request submitted"
                    };
                    await _emailSender.SendEmail(email);
                }
                catch
                {

                }
            }

            return response;
        }
    }
}
