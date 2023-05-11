using AutoMapper;
using LeaveManagement.Domain;
using LeaveManagement.Mvc.Models;
using LeaveManagement.Mvc.Services.Base;

namespace LeaveManagement.Mvc
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVm>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVm>().ReverseMap();

            CreateMap<RegisterVm,RegistrationRequest>().ReverseMap();

            CreateMap<Employee, EmployeeVm>().ReverseMap();

            CreateMap<CreateLeaveRequestDto,CreateLeaveRequestVm>().ReverseMap();
            CreateMap<LeaveRequestDto, LeaveRequestVm>()
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate.DateTime))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate.DateTime))
                .ForMember(d => d.DateRequested, opt => opt.MapFrom(d => d.DateRequested.DateTime))
                .ReverseMap();
            CreateMap<LeaveRequestListDto,LeaveRequestVm>()
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate.DateTime))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.StartDate.DateTime))
                .ForMember(d => d.DateRequested, opt => opt.MapFrom(d => d.DateRequested.DateTime))
                .ReverseMap();


        }
    }
}
