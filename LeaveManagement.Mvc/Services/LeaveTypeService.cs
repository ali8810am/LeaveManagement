using AutoMapper;
using LeaveManagement.Mvc.Contracts;
using LeaveManagement.Mvc.Models;
using LeaveManagement.Mvc.Services.Base;

namespace LeaveManagement.Mvc.Services
{
    public class LeaveTypeService: BaseHttpService,ILeaveTypeService
    {
        private readonly ILocalStorageService _storageService;
        private readonly IMapper _mapper;
        private readonly IClient _client;
        public LeaveTypeService(IMapper mapper, ILocalStorageService localStorageService, IClient client) : base(localStorageService, client)
        {
            _mapper = mapper;
            _client=client;
            _storageService = localStorageService;
        }


        public async Task<List<LeaveTypeVm>> GetLeaveTypes()
        {
            AddBearerToken();
            var leaveTypes =await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVm>>(leaveTypes);
        }

        public async Task<LeaveTypeVm> GetLeaveType(int id)
        {
            AddBearerToken();
            var leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeVm>(leaveType);
        }

        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVm leaveType)
        {
            try
            {
                var response = new Response<int>();
                var dto = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                AddBearerToken();
                var apiResponse= await _client.LeaveTypesPOSTAsync(dto);
                if (apiResponse.Success==true)
                {
                    response.Success = true;
                    response.Data = apiResponse.Id;
                    response.Message=apiResponse.Message;
                }
                else
                {
                    response.Success=false;
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
          
        }

        public async Task<Response<int>> UpdateLeaveType(LeaveTypeVm leaveType)
        {
            try
            {
                var dto = _mapper.Map<LeaveTypeDto>(leaveType);
                AddBearerToken();
                await _client.LeaveTypesPUTAsync(dto);
                return new Response<int> { Success = true };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                AddBearerToken();
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<int> { Success = true };
            }
            catch (ApiException e)
            {
                return ConvertApiExceptions<int>(e);
            }
        }
    }
}
