using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Constants;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Exceptions;
using LeaveManagement.Application.Models;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LeaveManagement.Identity.Services
{
    public class AuthService:IAuthService    
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _roleManager=roleManager;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username);


                if (user == null)
                {
                    throw new NotFoundException($"User with {request.Username} not found.", request.Username);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded == false)
                {
                    throw new BadRequestException($"Credentials for '{request.Username} aren't valid'.");
                }

                JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
                var response = new AuthResponse
                {
                    Id = user.Id,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email,
                    UserName = user.UserName
                };
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        
        }


        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };
            var exist =await _userManager.Users.AnyAsync(u =>
                u.UserName == request.UserName || u.Email.ToLower() == request.Email.ToLower());
            if (exist)
                throw new BadRequestException("User exist. please login");
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var employeeRoleExist = await _roleManager.RoleExistsAsync(UserRoles.Employee);
                if (employeeRoleExist == false)
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));
                var adminRoleExist = await _roleManager.RoleExistsAsync(UserRoles.Admin);
                if (adminRoleExist == false)
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                await _userManager.AddToRoleAsync(user,UserRoles.Employee);
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                StringBuilder str = new StringBuilder();
                foreach (var err in result.Errors)
                {
                    str.AppendFormat("•{0}\n", err.Description);
                }

                throw new BadRequestException($"{str}");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.UId, user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: _jwtSettings.Issuer,
               audience: _jwtSettings.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
               signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
