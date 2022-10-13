
using Infra.DTOs;
using Infra.Utili;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FilterAttributeHarougeAPI.Common
{
    public class ExceptionHandlerUtili : ActionFilterAttribute, IExceptionFilter
    {
        private readonly HelperUtili _helper;
        public ExceptionHandlerUtili(HelperUtili helper)
        {
            _helper = helper;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var r = _helper.GetCurrentUser();
            base.OnActionExecuting(context);
        }
        public async void OnException(ExceptionContext actionExecutedContext)
        {
            await Task.FromResult(true);
            var inner_Exception = "No Inner Exception";
            if (actionExecutedContext.Exception.InnerException != null)
            {
                inner_Exception = actionExecutedContext.Exception.InnerException.Message;
            }
            var result = new
            {
                Messages = actionExecutedContext.Exception.Message + " " + inner_Exception,

                ActionError = actionExecutedContext.ActionDescriptor.DisplayName,
            };
            var resultOperation = ResultOperationDTO<object>.CreateErrorOperation(messages: new string[] {
                result.Messages
            }, stateResult: StateResult.ex);

            var message =
                "\n\n\n ***********************************************" + DateTime.Now.ToString() + "*****************************************************" + "\n\n\n" +
                "Status Code                    ====> \t" + 400 + "\n\n\n" +
                "Path Error                     ====> \t" + actionExecutedContext.ActionDescriptor.DisplayName + "\n\n\n" +
                "Type Execut Exception          ====> \t" + "ExecuteException" + "\n\n\n" +
                "Exception Masseges             ====> \t" + actionExecutedContext.Exception.Message + "\n\n\n" +
                "inner Exception                ====> \t" + inner_Exception + "\n\n\n" +
                "Date Exception                 ====> \t" + DateTime.Now.ToString() + "\n\n\n" +
                "Source Exception               ====> \t" + actionExecutedContext.Exception.StackTrace + "\n\n\n";

            //await LogDataInFile(message);
            actionExecutedContext.Result = new OkObjectResult(resultOperation);
        }
        private async Task LogDataInFile(string data)
        {
            var pathFull = Path.GetFullPath("~/LoggingFile/");
            var pathFile = pathFull.Replace("~", "");
            var dateNow = "LogException" + DateTime.Now.ToString("yyyy_MM_dd");
            await File.AppendAllTextAsync(pathFile + dateNow + ".txt", data);
        }
    }
}