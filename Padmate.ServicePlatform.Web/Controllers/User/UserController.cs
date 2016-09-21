using Microsoft.AspNet.Identity;
using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.User
{
    [Authorize(Roles=SystemRole.SystemAdmin+","+SystemRole.BackstageAdmin)]
    public class UserController:BaseController
    {
        [HttpPost]
        public ActionResult GetPageData()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_User model = JsonHandler.UnJson<M_User>(strReqStream);

            B_User bUser = new B_User();
            var pageData = bUser.GetPageData(model);
            var totalCount = bUser.GetPageDataTotalCount(model);

            PageResult<M_User> pageResult = new PageResult<M_User>(totalCount, pageData);
            return Json(pageResult);
        }


        [HttpPost]
        public ActionResult GetUserById(string userid)
        {
            B_User bUser = new B_User();

            var user = bUser.GetUserById(userid); ;
            return Json(user);
        }

        public ActionResult Detail(string id)
        {
            B_User bUser = new B_User();

            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("找不到id为空的数据信息");
            }
            var user = bUser.GetUserById(id); ;

            ViewData["user"] = user;

            return View();
        }

        public ActionResult Add()
        {
            //找出所有角色
            B_Role bRole = new B_Role();
            var roles = bRole.GetAllData();
            ViewData["roles"] = roles;


            return View();

        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveAdd()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_User model = JsonHandler.DeserializeJsonToObject<M_User>(strReqStream);

            //默认密码为123456
            var defaultPawd = "123456";
            var passwordHash = new PasswordHasher().HashPassword(defaultPawd);
            model.PasswordHash = passwordHash;

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            B_User bUser = new B_User();
            message = bUser.AddUser(model);

            return Json(message);
        }

        public ActionResult Edit(string userId)
        {

            B_User bUser = new B_User();
            var user = bUser.GetUserById(userId);
            ViewData["user"] = user;

            //找出所有角色
            B_Role bRole = new B_Role();
            var roles = bRole.GetAllData();
            ViewData["roles"] = roles;


            return View();
        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveEdit()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_User model = JsonHandler.DeserializeJsonToObject<M_User>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            B_User bUser = new B_User();
            message = bUser.EditUser(model);

            return Json(message);

        }


        public ActionResult Delete(string UserId)
        {
            B_User bUser = new B_User();
            Message message = bUser.DeleteById(UserId);

            return Json(message);
        }

        [HttpPost]
        public ActionResult BachDeleteById()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            List<string> contactIds = JsonHandler.DeserializeJsonToObject<List<string>>(strReqStream);

            Message message = new Message();
            B_User bUser = new B_User();

            message = bUser.BatchDeleteByIds(contactIds);
            return Json(message);
        }
    }
}