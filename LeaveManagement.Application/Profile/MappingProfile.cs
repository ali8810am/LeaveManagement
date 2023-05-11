using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LeaveManagement.Application.Dtos.LeaveAllocation;
using LeaveManagement.Application.DTOs.LeaveAllocation;
using LeaveManagement.Application.DTOs.LeaveRequest;
using LeaveManagement.Application.Dtos.LeaveType;
using LeaveManagement.Application.DTOs.LeaveType;
using LeaveManagement.Domain;

namespace LeaveManagement.Application.Profile
{
    public class MappingProfile: AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation, CreateLeaveAllocationDto>().ReverseMap();


            CreateMap<LeaveRequest, LeaveRequestListDto>()
                .ForMember(dest=>dest.DateRequested,option=>option.MapFrom(src=>src.DateCreated))
                .ForMember(d=>d.RequestingEmployeeId,opt=>opt.MapFrom(src=>src.RequestngEmployeeId))
                .ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestDto>()
                .ForMember(dest => dest.DateRequested, option => option.MapFrom(src => src.DateCreated))
                .ForMember(d => d.RequestingEmployeeId, opt => opt.MapFrom(src => src.RequestngEmployeeId))
                .ReverseMap(); ;
            CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();


            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();
        }
    }
}
