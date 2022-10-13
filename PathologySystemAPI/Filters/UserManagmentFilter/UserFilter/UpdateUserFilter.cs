using Infra.DTOs;
using Infra.ValidationServices.IUserManagmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HarougeAPI.Models.UserManagmentModel.UserModel;
using System.Threading.Tasks;

namespace HarougeAPI.Filters.UserManagmentFilter.UserFilter
{
    public class UpdateUserFilter : ActionFilterAttribute
    {
        private readonly IUserValidationServices _validationServices;
        public UpdateUserFilter(IUserValidationServices validationServices)
        {
            _validationServices = validationServices;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            var param = context.ActionArguments.TryGetValue("updateModel", out var _updateModel);

            if (_updateModel is UpdateUserModel updateModel)
            {

                if (await _validationServices.IsDelete(updateModel.Id))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "اسم المستخدم تم أرشفته من قبل مستخدم أخر" }));
                    return;
                }

                if (await _validationServices.CheckUserName(updateModel.Id, updateModel.Name))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "اسم المستخدم موجود مسبثا" }));
                    return;
                }

                if (await _validationServices.CheckUserEmail(updateModel.Id, updateModel.Email))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                        CreateErrorOperation(messages: new string[] { "بريد الإلكتروني موجود مسبثا" }));
                    return;
                }

            }



            await base.OnActionExecutionAsync(context, next);
        }
    }
}
