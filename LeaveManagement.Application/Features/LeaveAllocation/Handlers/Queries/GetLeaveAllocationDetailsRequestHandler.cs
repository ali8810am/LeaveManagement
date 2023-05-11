using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Contracts.Presistence;
using LeaveManagement.Application.Dtos.LeaveAllocation;
using LeaveManagement.Application.DTOs.LeaveAllocation;
using LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using LeaveManagement.Application.Features.LeaveTypes.Request.Queries;

using MediatR;

namespace LeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
    public class GetLeaveAllocationDetailsRequestHandler:IRequestHandler<GetLeaveAllocationDetailsRequest,LeaveAllocationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveAllocationDetailsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocationDetails =await _unitOfWork.LeaveAllocationRepository.Get(request.Id);
            return _mapper.Map<LeaveAllocationDto>(leaveAllocationDetails);
        }
    }
}
