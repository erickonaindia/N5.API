using MediatR;
using System.Text.Json.Serialization;

namespace N5.Application.Commands.Permission
{
    public record ModifyPermissionCommand : IRequest<ModifyPermissionResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
