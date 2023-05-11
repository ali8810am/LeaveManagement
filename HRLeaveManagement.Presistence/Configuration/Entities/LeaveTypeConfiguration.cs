using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagement.Presistence.Configuration.Entities
{
    public class LeaveTypeConfiguration:IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Vacation",
                    CreatedBy = "Admin",
                    LastModifiedBy = "Admin"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 12,
                    Name = "Sick",
                    CreatedBy = "Admin",
                    LastModifiedBy = "Admin"
                }
            );
        }
    }
}
