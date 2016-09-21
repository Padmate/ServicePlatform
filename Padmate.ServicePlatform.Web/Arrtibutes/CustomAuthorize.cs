using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Arrtibutes
{
    public class CustomAuthorize:AuthorizeAttribute
    {
        // NOTE: This is not thread safe, it is much better to store this
        // value in HttpContext.Items.  See Ben Cull's answer below for an example.
        private bool _isAuthorized;

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            _isAuthorized = base.AuthorizeCore(httpContext);
            return _isAuthorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!_isAuthorized)
            {
                var strContent = "<html xmlns='http://www.w3.org/1999/xhtml' >";
                strContent += "<head runat='server'>";
                strContent += "<title>Unauthorized</title>";
                strContent += "</head>";
                strContent += "<body>";
                strContent += "<script>";
                strContent += "alert('对不起，您没有权限！');";


                strContent += "</script>";
                strContent += "</body>";
                strContent += "</html>";

                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Write(strContent);
                filterContext.HttpContext.Response.End();
                filterContext.Result = new EmptyResult();

            }
        }
    }
}