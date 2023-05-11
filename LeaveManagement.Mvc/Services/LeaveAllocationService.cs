using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagement.Mvc.Contracts;
using LeaveManagement.Mvc.Services.Base;

namespace LeaveManagement.Mvc.Services
{
    public class LeaveAllocationService: BaseHttpService, ILeaveAllocationService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorageService;

        public LeaveAllocationService(IClient client, ILocalStorageService localStorageService)
            : base(localStorageService, client)
        {
           this._client = client;
           this._localStorageService = localStorageService;
        }
        public async Task<Response<int>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                AddBearerToken();
                var leaveAllocationDto= new CreateLeaveAllocationDto{LeaveTypeId = leaveTypeId};
                var response=new Response<int>();
                var apiResponse= await _client.LeaveAllocationPOSTAsync(leaveAllocationDto);
                if (apiResponse.Success)
                    response.Success = true;
                else
                {
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
    }
}
