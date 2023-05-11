using LeaveManagement.Domain;
using LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Presistence
{
    public class ApplicationDbContext:AuditableDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
    }
}