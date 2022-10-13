using FluentValidation;
using HarougeAPI.Models.UserManagmentModel.PermisstionModel;

namespace HarougeAPI.ValidationModel.UserManagmentValidationModel.PermisstionValidationModel
{
    public class InsertPermisstionValidationModel : AbstractValidator<InsertPermisstionModel>
    {
        public InsertPermisstionValidationModel()
        {
            RuleFor(role => role.Name).NotEmpty().WithMessage("يجب إدخال اسم الصلاحية بالإنجليزي");
            RuleFor(role => role.Description).NotEmpty().WithMessage("يجب إدخال وصف الصلاحية");
            RuleFor(role => role.ModuleId).NotEmpty().WithMessage("يجب إختيار التبعية");
        }
    }
}
