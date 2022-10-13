using FluentValidation;
using HarougeAPI.Models.UserManagmentModel.UserModel;

namespace HarougeAPI.ValidationModel.UserManagmentValidationModel.UserValidationModel
{
    public class InsertUserValidationModel : AbstractValidator<InsertUserModel>
    {
        public InsertUserValidationModel()
        {
            //RuleFor(role => role.GroupId).NotEmpty().WithMessage("يجب إختيار التبيعية");
            RuleFor(role => role.Email).NotEmpty().WithMessage("يجب إدخال بريد الإلكتروني");
            RuleFor(role => role.Name).NotEmpty().WithMessage("يجب إدخال اسم المستخدم ");
            RuleFor(role => role.FullName).NotEmpty().WithMessage("يجب إدخال الاسم الرباعي  ");
            RuleFor(role => role.PasswordHash).NotEmpty().WithMessage("يجب إدخال كلمة المرور");
            RuleFor(role => role.RoleId).NotEmpty().WithMessage("يجب اختيار دور المستخدم");
        }
    }
}
