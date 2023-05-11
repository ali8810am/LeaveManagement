﻿using System.Net.Http.Headers;
using LeaveManagement.Mvc.Contracts;

namespace LeaveManagement.Mvc.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageService _localStorageService;
        protected IClient _client;

        public BaseHttpService(ILocalStorageService localStorageService, IClient client)
        {
            _localStorageService = localStorageService;
            _client = client;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            if (ex.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Validation errors have occured", ValidationErrors = ex.Response, Success = false };
            }
            else if (ex.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "The required item cannot be found", Success = false };
            }
            else
            {
                return new Response<Guid>() { Message = "Something went wrong please try again later", Success = false };
            }

        }

        protected void AddBearerToken()
        {
            if (_localStorageService.Exists("token"))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _localStorageService.GetStorageValue<string>("token"));
            }
        }
    }
}
