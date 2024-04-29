using FluentValidation;
using N5.Application.Commands.Permission;

namespace N5.Api.Validations.Permissions
{
    public class ModifyPermissionCommandValidation : AbstractValidator<ModifyPermissionCommand>
    {
        public ModifyPermissionCommandValidation()
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

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id field is required.")
                .NotEmpty().WithMessage("Id field can't be empty.");

        }
    }
}