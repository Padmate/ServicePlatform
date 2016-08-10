using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Padmate.ServicePlatform.API.Arrtibutes;
using Padmate.ServicePlatform.Web.Arrtibutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Padmate.ServicePlatform.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented  //返回格式缩进
                //ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // Remove the JSON formatter
            // 删除JSON格式化器
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
            // Remove the XML formatter
            // 删除XML格式化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //API全局异常处理
            config.Services.Replace(typeof(IExceptionHandler), new WebApiGlobalExceptionHandler());

        }
    }
}
