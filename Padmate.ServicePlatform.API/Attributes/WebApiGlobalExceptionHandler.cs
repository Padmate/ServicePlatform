using Padmate.ServicePlatform.API.Models;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace Padmate.ServicePlatform.API.Arrtibutes
{
    /// <summary>
    /// Web api 全局异常处理
    /// </summary>
    public class WebApiGlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            CustomerResponse response = new CustomerResponse();
            response.StatusCode =System.Convert.ToInt32(HttpStatusCode.InternalServerError);
            response.Message = "对不起，服务器发生错误。";

            
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = JsonHandler.ToJson(response)
            };
        }

        public virtual bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }

            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                                 new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(Content);
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }


}