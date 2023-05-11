using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Constants;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.DTOs.LeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Requests.Queries;

using MediatR;
using Microsoft.AspNetCore.Http;

namespace LeaveManagement.Application.Features.LeaveRequest.Handlers.Queries
{
    public class LeaveRequestListRequestHandler : IRequestHandler<LeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        public LeaveRequestListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        public async Task<List<LeaveRequestListDto>> Handle(LeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequestList = new List<Domain.LeaveRequest>();
            var leaveRequestDtos = new List<LeaveRequestListDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _contextAccessor.HttpContext.User.FindFirst(cl =>
                    cl.Type == CustomClaimTypes.UId)?.Value;
                leaveRequestList = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestWithDetails(userId);
                var employee=await _userService.GetEmployee(userId);

                leaveRequestDtos = _mapper.Map<List<LeaveRequestListDto>>(leaveRequestList);
                foreach (var req in leaveRequestDtos)
                {
                    req.Employee=employee;
                }
            }
            else
            {
                leaveRequestList = await _unitOfWork.LeaveRequestRepository.GetLeaveRequestsWithDetails();
                leaveRequestDtos = _mapper.Map<List<LeaveRequestListDto>>(leaveRequestList);
                foreach (var req in leaveRequestDtos)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }

            }
            return leaveRequestDtos;
        }
    }
}
