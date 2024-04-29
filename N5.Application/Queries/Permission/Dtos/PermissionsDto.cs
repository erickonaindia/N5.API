namespace N5.Application.Queries.Permission.Dtos
{
    public class PermissionsDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeLastName { get; set; } = string.Empty;
        public int PermissionType { get; set; }
        public DateTime PermissionDate { get; set; }
    }
}
