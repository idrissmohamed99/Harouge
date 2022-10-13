using FluentValidation;
using HarougeAPI.Models.UserManagmentModel;
using HarougeAPI.Models.UserManagmentModel.RoleModel;
using System.Collections.Generic;
using System.Linq;

namespace HarougeAPI.ValidationModel.UserManagmentValidationModel.RoleValidationModel
{
    public class UpdateRoleValidationModel : AbstractValidator<UpdateRoleModel>
    {
        public UpdateRoleValidationModel()
        {
            RuleFor(role => role.Id).NotEmpty().WithMessage("لم يتم إرسال رقم التعريف");
            RuleFor(role => role.ModuleId).NotEmpty().WithMessage("يجب إختيار التبيعة");
            RuleFor(role => role.Name).NotEmpty().WithMessage("يجب إدخال اسم الدور");
            RuleFor(role => role.RolePermisstion).Must(IsValue).WithMessage("يجب إختيار صلاحية واحدة علي الأقل");
        }
        private bool IsValue(List<RolePermisstionModel> value)
        {
            try
            {
                if (value.Count() > 0)
                    return true;
            }
            catch
            {
            }
            return false;
        }
    }
}
