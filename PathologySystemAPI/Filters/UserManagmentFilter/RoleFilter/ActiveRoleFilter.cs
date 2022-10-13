using Infra.DTOs;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.RoleFilter
{
    public class ActiveRoleFilter : ActionFilterAttribute
    {

        private readonly IRoleValidationServices _validationServices;
        public ActiveRoleFilter(IRoleValidationServices validationServices)
        {
            _validationServices = validationServices;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.TryGetValue("roleId", out var _roleId);

            if (!param)
            {
                context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { "لم يتم إرسال رقم التعريف الدور" }));
                return;
            }
            if (_roleId is string roleId)
            {
                if (!await _validationServices.IsDeleteRole(roleId))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "بيانات الدور تم حذفه" }));
                    return;
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
