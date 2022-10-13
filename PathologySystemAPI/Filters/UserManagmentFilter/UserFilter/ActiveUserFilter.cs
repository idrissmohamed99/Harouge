using Infra.DTOs;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.UserFilter
{
    public class ActiveUserFilter : ActionFilterAttribute
    {

        private readonly IUserValidationServices _validationServices;
        public ActiveUserFilter(IUserValidationServices validationServices)
        {
            _validationServices = validationServices;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.TryGetValue("userId", out var _userId);

            if (!param)
            {
                context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                                      CreateErrorOperation(messages: new string[] { "لم يتم إرسال رقم التعريف المستخدم" }));
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
