using MediatR;

namespace N5.Application.Commands.Permission.Request
{
    public record RequestPermissionCommand : IRequest<RequestPermissionResponse>
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
