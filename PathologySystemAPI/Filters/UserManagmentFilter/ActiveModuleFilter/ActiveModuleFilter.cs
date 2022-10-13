using Infra.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.ActiveModuleFilter
{
    public class ActiveModuleFilter : ActionFilterAttribute
    {

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.TryGetValue("moduleId", out var _moduleId);

            if (!param)
            {
                context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                       CreateErrorOperation(messages: new string[] { "لم يتم إرسال رقم التعريف التبعية" }));
                return;
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
