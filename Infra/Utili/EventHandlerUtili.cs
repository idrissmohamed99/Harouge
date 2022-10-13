using Infra.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Utili
{
    public class EventHandlerUtili : JwtBearerEvents
    {

        public async override Task TokenValidated(TokenValidatedContext context)
        {
            await base.TokenValidated(context);
        }

        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status200OK;
            var result = new
            {
                Messages = "انتهت صلاحية الدخول",
            };
            var resultOperation = ResultOperationDTO<string>.CreateErrorOperation(messages: new string[] {
                result.Messages
            }, stateResult: StateResult.FiledAuth);

            var json = JsonConvert.SerializeObject(resultOperation);
            context.Response.WriteAsync(json);
            return Task.CompletedTask;
        }

        //public override Task Forbidden(ForbiddenContext context)
        //{
        //    return base.Forbidden(context);
        //}

        public override Task Challenge(JwtBearerChallengeContext context)
        {
            string token;
            if (!TryRetrieveToken(context.Request, out token))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status200OK;
                var result = new
                {
                    Messages = "لاتلمك صلاحية الدخول أو انتهت صلاحية الدخول",
                };
                var resultOperation = ResultOperationDTO<string>.CreateErrorOperation(messages: new string[] {
                result.Messages
                }, stateResult: StateResult.UnAuth);
                var json = JsonConvert.SerializeObject(resultOperation);
                context.Response.WriteAsync(json);
            }
            return Task.CompletedTask;
        }

        public override Task MessageReceived(MessageReceivedContext context)
        {
            return base.MessageReceived(context);
        }

        private bool TryRetrieveToken(HttpRequest request, out string token)
        {
            token = null;
            Microsoft.Extensions.Primitives.StringValues authzHeaders;
            if (!request.Headers.TryGetValue("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }


            var bearerToken = authzHeaders.ElementAt(0);

            if (bearerToken == "Bearer undefined") return false;

            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

    }
}
