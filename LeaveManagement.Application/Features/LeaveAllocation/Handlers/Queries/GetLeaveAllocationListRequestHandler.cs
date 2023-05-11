using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Constants;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.Dtos.LeaveAllocation;
using LeaveManagement.Application.DTOs.LeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using LeaveManagement.Application.Models.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler:IRequestHandler<GetLeaveAllocationListRequest,List<LeaveAllocationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        public GetLeaveAllocationListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }


        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {

            var leaveAllocations = new List<Domain.LeaveAllocation>();
            var leaveAllocationsDto = new List<LeaveAllocationDto>();
            if (request.IsLoggedInUser)
            {
                var userId = _contextAccessor.HttpContext.User.FindFirst(cl => cl.Type == CustomClaimTypes.UId)?.Value;
                var employee=await _userService.GetEmployee(userId);
                leaveAllocations =await _unitOfWork.LeaveAllocationRepository.GetLeaveAllocationsWithDetails(userId);
                leaveAllocationsDto = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach (var dto in leaveAllocationsDto)
                {
                    dto.Employee = employee;
                }
            }
            else
            {
                leaveAllocations = await _unitOfWork.LeaveAllocationRepository.GetLeaveAllocationsWithDetails();
                leaveAllocationsDto = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach (var dto in leaveAllocationsDto)
                {
                    dto.Employee =await _userService.GetEmployee(dto.EmployeeId);
                }
            }

            return leaveAllocationsDto;
        }
    }
}
