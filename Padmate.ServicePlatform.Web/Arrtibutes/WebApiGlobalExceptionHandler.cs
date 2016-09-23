using log4net;
using log4net.Appender;
using log4net.Repository;
using Padmate.ServicePlatform.API.Models;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;

namespace Padmate.ServicePlatform.Web.Arrtibutes
{

    /// <summary>
    /// Web api 全局异常处理
    /// </summary>
    public class WebApiGlobalExceptionHandler : ExceptionHandler
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger("APILog");
       

        public override void Handle(ExceptionHandlerContext context)
        {
            CustomerResponse response = new CustomerResponse();
            response.StatusCode =System.Convert.ToInt32(HttpStatusCode.InternalServerError);
            response.Message = "对不起，服务器发生错误。";

            //修改日志存放路径
            //LogHelp.ChangeLoggerFile(LogHelp.APILogDirectory);
            //日志记录
            logger.Error("URL:"+context.Request.RequestUri,context.Exception);
            
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = JsonHandler.ToJson(response)
            };
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
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