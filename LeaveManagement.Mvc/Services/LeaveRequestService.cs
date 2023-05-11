using AutoMapper;
using Azure;
using LeaveManagement.Mvc.Contracts;
using LeaveManagement.Mvc.Models;
using LeaveManagement.MVC.Models;
using LeaveManagement.Mvc.Services.Base;

namespace LeaveManagement.Mvc.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly IMapper _mapper;
        private readonly IClient _client;

        public LeaveRequestService(ILocalStorageService localStorageService, IClient client, IMapper mapper) : base(localStorageService, client)
        {
            _mapper = mapper;
            _client = client;
        }
        public async Task<Base.Response<int>> CreateLeaveRequest(CreateLeaveRequestVm leaveRequest)
        {
            try
            {
                var response = new Base.Response<int>();
                CreateLeaveRequestDto request = _mapper.Map<CreateLeaveRequestDto>(leaveRequest);
                AddBearerToken();
                var apiResponse = await _client.LeaveRequestPOSTAsync(request);
                if (!apiResponse.Success)
                {
                    response.Success = false;
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                else
                {
                    response.Success=true;
                    response.Data = apiResponse.Id;
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }


        }

        public async Task<AdminLeaveRequestViewVm> GetAdminLeaveRequestList()
        {
            AddBearerToken();
            var requests =await _client.LeaveRequestAllAsync(isLoggedInUser: false);
            return new AdminLeaveRequestViewVm
            {
                TotalRequests = requests.Count,
                ApprovedRequests = requests.Count(r => r.Approved == true),
                LeaveRequests = _mapper.Map<List<LeaveRequestVm>>(requests),
                PendingRequests = requests.Count(r => r.Approved == null),
                RejectedRequests = requests.Count(r => r.Approved == false)
            };
        }

        public async Task<EmployeeLeaveRequestViewVm> GetUserLeaveRequests()
        {
            AddBearerToken();
            var leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: true);
            var allocations =await _client.LeaveRequestAllAsync(isLoggedInUser: true);
            var model = new EmployeeLeaveRequestViewVm
            {
                LeaveAllocations = _mapper.Map<List<LeaveAllocationVm>>(leaveRequests),
                LeaveRequests = _mapper.Map<List<LeaveRequestVm>>(leaveRequests)
            };
            return model;
        }

        public async Task<LeaveRequestVm> GetLeaveRequest(int id)
        {
            AddBearerToken();
            var leaveRequest = await _client.LeaveRequestGETAsync(id);
            return _mapper.Map<LeaveRequestVm>(leaveRequest);
        }

        public Task DeleteLeaveRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task ApproveLeaveRequest(int id, bool approved)
        {
            AddBearerToken();
            try
            {
                var changeApprovalDto = new ChangeLeaveRequestApprovalDto { Approved = approved, Id = id };
                await _client.ChangeapprovalAsync(id, changeApprovalDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
