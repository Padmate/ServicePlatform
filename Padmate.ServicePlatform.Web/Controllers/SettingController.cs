using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Utility;
using System.IO;

namespace Padmate.ServicePlatform.Web.Controllers
{
    [Authorize]
    public class SettingController:BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public SettingController()
        {
        }

        public SettingController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #region 用户信息
        public ActionResult UserInfo()
        {

            ViewData["UserInfo"] = this.GetCurrentUser(); ;

            return View();
        }
        #endregion

        #region
        /// <summary>
        /// 密码修改
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SetPassword()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            ChangePasswordViewModel model = JsonHandler.DeserializeJsonToObject<ChangePasswordViewModel>(strReqStream);

            Message message = new Message();
            message.Success = true;
            
            try
            {
                message = model.validate();
                if (!message.Success)
                    return Json(message);
                if (!model.NewPassword.Equals(model.ConfirmPassword))
                {
                    message.Success = false;
                    message.Content = "新密码与确认密码不匹配。";
                    return Json(message);

                }
                var userid = User.Identity.GetUserId();
                var result = await UserManager.ChangePasswordAsync(userid, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        //清除登录cookie信息
                        HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                    }
                }
                else
                {
                    message.Success = false;
                    message.Content = result.Errors.First().ToString();
                }
            }
            catch
            {

                message.Success = false;
                message.Content = "密码更改失败。";

            }
            return Json(message);
        }

        #endregion
    }
}