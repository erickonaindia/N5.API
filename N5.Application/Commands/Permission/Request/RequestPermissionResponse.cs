using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Application.Commands.Permission.Request
{
    public record RequestPermissionResponse
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
