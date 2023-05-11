using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Presistence
{
    public class AuditableDbContext:DbContext
    {
        public AuditableDbContext(DbContextOptions options) : base(options)
        {
        }
        public  virtual async Task<int> SaveChangesAsync(string username="system")
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                entry.Entity.LastModifiedBy = username;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = username;
                }
            }
            var result=await base.SaveChangesAsync();
            return result;
        }

    }
}
