using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers
{
    public class ContactController:BaseController
    {
        #region 职责范围
        public ActionResult Scope()
        {
            return View();
        }

        public ActionResult GetScopePageData()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_ContactScope model = JsonHandler.UnJson<M_ContactScope>(strReqStream);

            B_ContactScope bContactScope = new B_ContactScope();
            var pageData = bContactScope.GetPageData(model);
            var totalCount = bContactScope.GetPageDataTotalCount(model);

            PageResult<M_ContactScope> pageResult = new PageResult<M_ContactScope>(totalCount, pageData);
            return Json(pageResult);
        }

        public ActionResult AddScope()
        {
            return View();
        }

        // POST:
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveAddScope()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_ContactScope model = JsonHandler.DeserializeJsonToObject<M_ContactScope>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            B_ContactScope bContactScope = new B_ContactScope();
            //判断是否已存在该Scope
            var existScope = bContactScope.GetByScope(model.Scope.Trim());
            if(existScope !=null)
            {
                message.Success = false;
                message.Content = "已存在职责范围为："+model.Scope+"的数据，不能重复添加。";
                return Json(message);
            }

            message = bContactScope.AddContactScope(model);

            return Json(message);
        }


        public ActionResult EditScope(string id)
        {
            B_ContactScope bContactScope = new B_ContactScope();

            Int32 contactId = System.Convert.ToInt32(id);
            var contactScope = bContactScope.GetById(contactId);

            ViewData["ContactScope"] = contactScope;

            return View();
        }

        // POST:
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveEditScope()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_ContactScope model = JsonHandler.DeserializeJsonToObject<M_ContactScope>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            B_ContactScope bContactScope = new B_ContactScope();
            //修改前判断当前将要更新的Scope是否已存在系统中
            var existScope = bContactScope.GetByScope(model.Scope.Trim());
            if (existScope != null && existScope.Id != model.Id)
            {
                message.Success = false;
                message.Content = "已存在职责范围为：" + model.Scope + "的数据。";
                return Json(message);

            }

            message = bContactScope.EditContactScope(model);

            return Json(message);
        }

        [HttpPost]
        public ActionResult BachDeleteScopeById()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            List<string> contactScopeIds = JsonHandler.DeserializeJsonToObject<List<string>>(strReqStream);

            List<int> ids = new List<int>();
            foreach (var contactScopeid in contactScopeIds)
            {
                ids.Add(System.Convert.ToInt32(contactScopeid));
            }
            Message message = new Message();
            B_ContactScope bContactScope = new B_ContactScope();
            message = bContactScope.BatchDeleteByIds(ids);
            return Json(message);
        }

        #endregion

        #region 联系人信息
        public ActionResult Info()
        {
            return View();
        }


        public ActionResult GetPageData()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Contact model = JsonHandler.UnJson<M_Contact>(strReqStream);

            B_Contact bContact = new B_Contact();
            var pageData = bContact.GetPageData(model);
            var totalCount = bContact.GetPageDataTotalCount(model);

            PageResult<M_Contact> pageResult = new PageResult<M_Contact>(totalCount, pageData);
            return Json(pageResult);
        }

        public ActionResult Add()
        {
            B_ContactScope bContactScope = new B_ContactScope();
            var allScopes = bContactScope.GetAllData();
            ViewData["Scopes"] = allScopes;

            return View();
        }

        // POST:
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveAdd()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Contact model = JsonHandler.DeserializeJsonToObject<M_Contact>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            B_Contact bContact = new B_Contact();
            message = bContact.AddContact(model);

            return Json(message);
        }


        public ActionResult Edit(string id)
        {
            B_Contact bContact = new B_Contact();

            Int32 contactId = System.Convert.ToInt32(id);
            var contact = bContact.GetById(contactId);

            ViewData["Contact"] = contact;

            B_ContactScope bContactScope = new B_ContactScope();
            var allScopes = bContactScope.GetAllData();
            ViewData["Scopes"] = allScopes;

            return View();
        }

        // POST:
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveEdit()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Contact model = JsonHandler.DeserializeJsonToObject<M_Contact>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            B_Contact bContact = new B_Contact();
            message = bContact.EditContact(model);

            return Json(message);
        }

        [HttpPost]
        public ActionResult BachDeleteById()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            List<string> contactIds = JsonHandler.DeserializeJsonToObject<List<string>>(strReqStream);

            List<int> ids = new List<int>();
            foreach (var contactid in contactIds)
            {
                ids.Add(System.Convert.ToInt32(contactid));
            }
            Message message = new Message();
            B_Contact bContact = new B_Contact();
            message = bContact.BatchDeleteByIds(ids);
            return Json(message);
        }
        #endregion

        // POST:
        [HttpPost]
        public ActionResult SendMail()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Mail model = JsonHandler.DeserializeJsonToObject<M_Mail>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            if(string.IsNullOrEmpty(model.Creator))
            {
                message.Success = false;
                message.Content = "请输入您的姓名";
                return Json(message);
            }

            B_Mail bMail = new B_Mail();
            message = bMail.AddMail(model);

            if (message.Success) message.Content = "邮件发送成功，我们将会尽快给您回复。";
            return Json(message);
        }
    }
}