using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.DTOs.LeaveRequest;
using LeaveManagement.Application.Features.LeaveRequest.Requests.Queries;
using MediatR;

namespace LeaveManagement.Application.Features.LeaveRequest.Handlers.Queries
{
    public class LeaveRequestDetailsRequestHandler:IRequestHandler<LeaveRequestDetailsRequest,LeaveRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public LeaveRequestDetailsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }


        public async Task<LeaveRequestDto> Handle(LeaveRequestDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveRequestDetails =_mapper.Map<LeaveRequestDto>(await _unitOfWork.LeaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
            leaveRequestDetails.Employee =await _userService.GetEmployee(leaveRequestDetails.RequestingEmployeeId);
            return leaveRequestDetails;
        }
    }
}
