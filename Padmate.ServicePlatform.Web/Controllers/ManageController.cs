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
    public class ManageController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpPost]
        public ActionResult CKEditorUpload(HttpPostedFileBase upload)
        {
            var viirtualPath = SystemConfig.Init.PathConfiguration["editContentImageVirtualDirectory"].ToString();
            var fileName = System.IO.Path.GetFileName(upload.FileName);
            var filePhysicalDirectory = Server.MapPath("~" + viirtualPath);
            var filePhysicalPath = Path.Combine(filePhysicalDirectory, fileName); ;//我把它保存在网站根目录的 upload 文件夹

            //如果没有文件夹，则先新建文件夹
            if (!System.IO.Directory.Exists(filePhysicalDirectory))
            {
                System.IO.Directory.CreateDirectory(filePhysicalDirectory);

            }

            upload.SaveAs(filePhysicalPath);

            var url = Path.Combine(viirtualPath, fileName);
            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

            //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
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

        /// <summary>
        /// 首页图片管理
        /// </summary>
        /// <returns></returns>
        public ActionResult HomeBackground()
        {

            return View();
        }

        #region 服务管理
        /// <summary>
        /// 市场平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceMarket()
        {

            return View();
        }

        #region 产品平台
       
        /// <summary>
        /// 众创项目
        /// </summary>
        /// <returns></returns>
        public ActionResult ZZProject()
        {

            return View();
        }
        #endregion

        /// <summary>
        /// 工程平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceEngineer()
        {

            return View();
        }

        #endregion

        #region 活动管理

        /// <summary>
        /// 精彩活动
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivityForecast()
        {

            return View();
        }

        /// <summary>
        /// 活动预告
        /// </summary>
        /// <returns></returns>
        public ActionResult WonderfulActivity()
        {

            return View();
        }
        #endregion

        #region 资讯管理

        /// <summary>
        /// 资讯管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Information()
        {

            return View();
        }
        #endregion

        #region 用户管理
        public ActionResult UserManage()
        {
            return View();
        }
        #endregion

        public ActionResult ContactInfo()
        {
            return View();
        }

        public ActionResult ContactScope()
        {
            return View();
        }

        public ActionResult Mail()
        {
            return View();
        }
    }
}