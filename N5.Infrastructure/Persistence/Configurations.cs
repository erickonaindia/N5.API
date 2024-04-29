using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5.Domain.Entities;

namespace N5.Infrastructure.Persistence
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.EmployeeName).IsRequired();
            builder.Property(p => p.EmployeeLastName).IsRequired();
            builder.Property(p => p.PermissionDate).IsRequired();

            builder.HasOne(p => p.PermissionType)
                .WithMany(t => t.Permissions)
                .HasForeignKey(p => p.PermissionTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
