using Infra.DTOs;
using Infra.Utili;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HarougeAPI.Models.UserManagmentModel.RoleModel;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.RoleFilter
{
    public class UpdateRoleFilter : ActionFilterAttribute
    {

        private readonly IRoleValidationServices _validationServices;
        private readonly HelperUtili _helper;
        public UpdateRoleFilter(IRoleValidationServices validationServices, HelperUtili helper)
        {
            _validationServices = validationServices;
            _helper = helper;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.TryGetValue("updateModel", out var _updateModel);

            if (_updateModel is UpdateRoleModel updateModel)
            {
                updateModel.ModuleId = string.IsNullOrEmpty(updateModel.ModuleId) ?
                   _helper.GetCurrentUser().ModuleId : updateModel.ModuleId;

                if (!await _validationServices.IsDeleteRole(updateModel.Id))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "اسم الدور تم حذفه" }));
                    return;
                }

                if (await _validationServices.CheckRoleName(updateModel.Id, updateModel.Name, updateModel.ModuleId))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "اسم الدور تم إدخاله مسبقا" }));
                    return;
                }

            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
