using FluentValidation;
using N5.Application.Commands.Permission.Request;

namespace N5.Api.Validations.Permissions
{
    public class RequestPermissionCommandValidation : AbstractValidator<RequestPermissionCommand>
    {
        public RequestPermissionCommandValidation()
        {
            RuleFor(x => x.EmployeeName)
                .NotNull().WithMessage("Employee Name field is required.")
                .NotEmpty().WithMessage("Employee Name field can't be empty.");

            RuleFor(x => x.EmployeeLastName)
                .NotNull().WithMessage("Employee Last Name field is required.")
                .NotEmpty().WithMessage("Employee Last Name field can't be empty.");

            RuleFor(x => x.PermissionDate)
                .NotNull().WithMessage("Permission Date field is required.")
                .NotEmpty().WithMessage("Permission Date field can't be empty.");

            RuleFor(x => x.PermissionType)
                .NotNull().WithMessage("Permission Type field is required.")
                .NotEmpty().WithMessage("Permission Type field can't be empty.");

        }
    }
}