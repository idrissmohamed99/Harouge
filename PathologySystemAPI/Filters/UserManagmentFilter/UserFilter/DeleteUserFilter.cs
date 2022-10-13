using Infra.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.UserFilter
{
    public class DeleteUserFilter : ActionFilterAttribute
    {

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.TryGetValue("userId", out var _userId);

            if (!param)
            {
                context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                                      CreateErrorOperation(messages: new string[] { "لم يتم إرسال رقم التعريف المستخدم" }));
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
