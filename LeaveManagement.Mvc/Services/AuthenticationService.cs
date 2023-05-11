using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Mvc.Contracts;
using LeaveManagement.Mvc.Models;
using LeaveManagement.Mvc.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AuthRequest = LeaveManagement.Mvc.Services.Base.AuthRequest;
using IAuthenticationService = LeaveManagement.Mvc.Contracts.IAuthenticationService;
using RegistrationRequest = LeaveManagement.Mvc.Services.Base.RegistrationRequest;

namespace LeaveManagement.Mvc.Services
{
    public class AuthenticationService:BaseHttpService,IAuthenticationService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IMapper _mapper;

        public AuthenticationService(IMapper mapper ,ILocalStorageService localStorageService, IClient client, IHttpContextAccessor contextAccessor) 
            : base(localStorageService, client)
        {
            this._contextAccessor = contextAccessor;
            this._jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _mapper=mapper;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            try
            {
                _localStorageService.ClearStorage(new List<string> { "token" });
                AuthRequest authRequest = new AuthRequest { Username = username, Password = password };
                var authResponse = await _client.LoginAsync(authRequest);
                if (authResponse.Token != null)
                {
                    var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(authResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _contextAccessor.HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorageService.SetStorageValue(authResponse.Token, "token");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ApiException e)
            {
                return false;
            }
        }

        public async Task<bool> Register(RegisterVm register)
        {
            var registrationRequest = _mapper.Map<RegistrationRequest>(register);
            var response=await _client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            _localStorageService.ClearStorage(new List<string>{"token"});
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims=tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name,tokenContent.Subject));
            return claims;
        }
    }
}
