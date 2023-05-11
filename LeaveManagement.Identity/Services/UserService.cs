using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Constants;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Identity.Services
{
    public class UserService:IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            var employees =await _userManager.GetUsersInRoleAsync(UserRoles.Employee);
            return employees.Select(e => new Employee
            {
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Id = e.Id
            }).ToList();
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return new Employee
            {
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Id = employee.Id
            };
        }
    }
}
