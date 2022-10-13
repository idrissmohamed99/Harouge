using FluentValidation;
using HarougeAPI.Models.UserManagmentModel.UserModel;

namespace HarougeAPI.ValidationModel.UserManagmentValidationModel.UserValidationModel
{
    public class UpdateUserValidationModel : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserValidationModel()
        {
            RuleFor(role => role.Email).NotEmpty().WithMessage("يجب إدخال بريد الإلكتروني");
            RuleFor(role => role.Name).NotEmpty().WithMessage("يجب إدخال اسم المستخدم ");
            RuleFor(role => role.FullName).NotEmpty().WithMessage("يجب إدخال الاسم الرباعي  ");
            RuleFor(role => role.RoleId).NotEmpty().WithMessage("يجب اختيار دور المستخدم");
            RuleFor(role => role.Id).NotEmpty().WithMessage("لم يتم إرسال رقم التعريف المستخدم");
        }
    }
}
