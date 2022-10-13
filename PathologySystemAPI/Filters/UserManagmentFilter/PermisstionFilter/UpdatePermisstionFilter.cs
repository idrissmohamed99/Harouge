using Infra.DTOs;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HarougeAPI.Models.UserManagmentModel.PermisstionModel;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.PermisstionFilter
{
    public class UpdatePermisstionFilter : ActionFilterAttribute
    {
        private readonly IPermisstionValidationServices _validationServices;
        public UpdatePermisstionFilter(IPermisstionValidationServices validationServices)
        {
            _validationServices = validationServices;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.TryGetValue("updateModel", out var _updateModel);

            if (_updateModel is UpdatePermisstionModel updateModel)
            {
                if (await _validationServices.IsDescriptionExists(updateModel.Id, updateModel.Description, updateModel.ModuleId))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "وصف الصلاحية لنفس التبعية تم إدخال مسبقا" }));
                    return;
                }
                if (await _validationServices.IsNameExists(updateModel.Id, updateModel.Name))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "اسم الصلاحية تم إدخال مسبقا" }));
                    return;
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
