using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace N5.Domain.Entities
{
    public class PermissionType : EntityBase
    {
        public string Description { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; }
    }
}
