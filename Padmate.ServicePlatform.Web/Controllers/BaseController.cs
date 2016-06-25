using Padmate.ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers
{
    public class BaseController:Controller
    {
        public string ModelStateError()
        {
            string errorMessageStr = string.Empty;

            var errorMessages = from state in ModelState.Values
                                from error in state.Errors
                                select error.ErrorMessage;

            var errorList = errorMessages.ToList();

            string errorMessage = string.Empty;
            foreach (string error in errorList)
            {
                errorMessage += error + "<br/>";
            }

            return errorMessage;
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public UserInfo GetCurrentUser()
        {
            UserInfo user = null;
            if(User.Identity != null)
            {
                user = new UserInfo();
                user.UserName = User.Identity.Name;

            }

            return user;
        }
    }
}