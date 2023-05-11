using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using LeaveManagement.Application.Responses;
using LeaveManagement.Domain;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler:IRequestHandler<CreateLeaveAllocationCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public CreateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }


        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Errors = validationResult.Errors.Select(r => r.ErrorMessage).ToList();
                response.Message = "Creation Failed";
            }
            else
            {
                var leaveType = await _unitOfWork.LeaveTypeRepository.Get(request.LeaveAllocationDto.LeaveTypeId);
                var employees = await _userService.GetEmployees();
                var period = DateTime.Now.Year;
                var allocations = new List<Domain.LeaveAllocation>();
                foreach (var employee in employees)
                {
                    if (await _unitOfWork.LeaveAllocationRepository.AllocationExist(employee.Id, leaveType.Id, period))
                        continue;
                    allocations.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = employee.Id,
                        Period = period,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays
                    });
                }

                await _unitOfWork.LeaveAllocationRepository.AddAllocations(allocations);
                response.Success = true;
                response.Message = "Creation succeed";
                await _unitOfWork.Save();
            }

            return response;
        }
    }
}
