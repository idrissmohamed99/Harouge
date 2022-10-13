using Infra.DTOs;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.UserFilter
{
    public class ResetPasswordFilter : ActionFilterAttribute
    {

        private readonly IUserValidationServices _validationServices;
        public ResetPasswordFilter(IUserValidationServices validationServices)
        {
            _validationServices = validationServices;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userIdParam = context.ActionArguments.TryGetValue("userId", out var _userId);
            var newPasswordParam = context.ActionArguments.TryGetValue("newPassword", out var _newPassword);

            if (!userIdParam)
            {
                context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                    CreateErrorOperation(messages: new string[] { "لم يتم إرسال رقم المستخدم " }));

                return;
            }

            if (!newPasswordParam)
            {
                context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                    CreateErrorOperation(messages: new string[] { "لم يتم إرسال كلمة المرور " }));
                return;
            }

            if (_userId is string userId)
            {
                if (await _validationServices.IsDelete(userId))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                                           CreateErrorOperation(messages: new string[] { "اسم المستخدم تم أرشفته من قبل مستخدم أخر" }));
                    return;
                }
            }


            await base.OnActionExecutionAsync(context, next);
        }
    }
}
