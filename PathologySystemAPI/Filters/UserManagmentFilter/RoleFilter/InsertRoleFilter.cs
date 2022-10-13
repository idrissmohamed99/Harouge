using Infra.DTOs;
using Infra.Utili;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HarougeAPI.Models.UserManagmentModel.RoleModel;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.RoleFilter
{
    public class InsertRoleFilter : ActionFilterAttribute
    {

        private readonly IRoleValidationServices _validationServices;
        private readonly HelperUtili _helper;
        public InsertRoleFilter(IRoleValidationServices validationServices, HelperUtili helper)
        {
            _validationServices = validationServices;
            _helper = helper;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.TryGetValue("insertModel", out var _insertModel);

            if (_insertModel is InsertRoleModel insertModel)
            {
                insertModel.ModuleId = string.IsNullOrEmpty(insertModel.ModuleId) ?
                   _helper.GetCurrentUser().ModuleId : insertModel.ModuleId;

                if (await _validationServices.CheckRoleName(insertModel.Name, insertModel.ModuleId))
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
