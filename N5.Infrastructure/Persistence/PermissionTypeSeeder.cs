using Microsoft.EntityFrameworkCore;
using N5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Infrastructure.Persistence
{
    public static class PermissionTypeSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var permissionTypes = new List<PermissionType>
            {
                new PermissionType { Id = 1, Description = "Type 1" },
                new PermissionType { Id = 2, Description = "Type 2" },
                new PermissionType { Id = 3, Description = "Type 3" }
            };

            modelBuilder.Entity<PermissionType>().HasData(permissionTypes);
        }
    }
}
