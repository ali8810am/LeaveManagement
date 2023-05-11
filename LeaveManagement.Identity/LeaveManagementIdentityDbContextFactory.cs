using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Identity
{
    //public class LeaveManagementIdentityDbContextFactory
    //{
    //    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<LeaveManagementIdentityDbContext>
    //    {
    //        public LeaveManagementIdentityDbContext CreateDbContext(string[] args)
    //        {
    //            IConfigurationRoot configuration = new ConfigurationBuilder()
    //                .SetBasePath(Directory.GetCurrentDirectory())
    //                .AddJsonFile("appsettings.json")
    //                .Build();
    //            var builder = new DbContextOptionsBuilder<LeaveManagementIdentityDbContext>();
    //            var connectionString = configuration.GetConnectionString("LeaveManagementConnectionString");
    //            builder.UseSqlServer(connectionString);
    //            return new LeaveManagementIdentityDbContext(builder.Options);
    //        }
    //    }
    //}
}
