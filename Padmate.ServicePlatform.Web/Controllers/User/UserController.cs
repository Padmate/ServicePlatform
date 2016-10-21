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
    [Authorize]
    public class UserController:BaseController
    {
        [HttpPost]
        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
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
        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
        public ActionResult GetUserById(string userid)
        {
            B_User bUser = new B_User();

            var user = bUser.GetUserById(userid); ;
            return Json(user);
        }

        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
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

        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
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
        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
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

        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
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
        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
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


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SetUserInfo()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            SetUserInfoModel model = JsonHandler.DeserializeJsonToObject<SetUserInfoModel>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            B_User bUser = new B_User();
            message = bUser.SetUserInfo(model);

            return Json(message);

        }

        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
        public ActionResult Delete(string UserId)
        {
            B_User bUser = new B_User();
            Message message = bUser.DeleteById(UserId);

            return Json(message);
        }

        [HttpPost]
        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
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


        /// <summary>
        /// 分片上传附件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChunkedUploadAttachment(HttpPostedFileBase file)
        {
            //虚拟目录
            string attachmentVirtualDirectory = SystemConfig.Init.PathConfiguration["userAttachmentVirtualDirectory"].ToString();

            Message message = new Message();
            message.Success = true;

            if (file == null)
            {
                message.Success = false;
                message.Content = "未获取到文件信息,请重新尝试";
                return Json(message);
            }

            try
            {
                FileInfo fileInfo = new FileInfo(file.FileName);


                int index = Convert.ToInt32(Request["chunk"]);//当前分块序号
                var guid = Request["guid"];//前端传来的GUID号
                var dir = Server.MapPath("~" + attachmentVirtualDirectory);//文件上传目录
                dir = Path.Combine(dir, guid + fileInfo.Name);//临时保存分块的目录

                if (!System.IO.Directory.Exists(dir))
                    System.IO.Directory.CreateDirectory(dir);

                //分块文件名为索引名，更严谨一些可以加上是否存在的判断，防止多线程时并发冲突
                string filePath = Path.Combine(dir, index.ToString());

                if (!System.IO.File.Exists(filePath))
                {
                    file.SaveAs(filePath);
                }

                message.Success = true;

            }
            catch (Exception e)
            {

                message.Success = false;
                message.Content = e.Message;

            }

            return Json(message);

        }

        /// <summary>
        /// 合并分片上传的文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MergeFile(M_UserAttachment model)
        {
            Message message = new Message();
            message.Success = true;

            string attachmentVirtualDirectory = SystemConfig.Init.PathConfiguration["userAttachmentVirtualDirectory"].ToString();

            #region 校验
            if (string.IsNullOrEmpty(model.UserId))
            {
                message.Success = false;
                message.Content = "未获取项目ID，请重新尝试";
                return Json(message);
            }

            #endregion

            var guid = Request["guid"];//GUID
            var uploadDir = Server.MapPath("~" + attachmentVirtualDirectory);//Upload 文件夹
            var fileName = Request["fileName"];//文件名
            var dir = Path.Combine(uploadDir, guid + fileName);//临时文件夹

            var extension = Request["extension"];
            var saveName = Guid.NewGuid().ToString() + "." + extension; //新的文件保存名称


            var files = System.IO.Directory.GetFiles(dir);//获得下面的所有分片文件
            var finalPath = Path.Combine(uploadDir, saveName);
            var fs = new FileStream(finalPath, FileMode.Create);
            foreach (var part in files.OrderBy(x => x))//排一下序，保证从0-N Write
            {
                if (System.IO.File.Exists(part))
                {
                    var bytes = System.IO.File.ReadAllBytes(part);
                    fs.Write(bytes, 0, bytes.Length);
                    bytes = null;
                    System.IO.File.Delete(part);//删除分块
                }

            }
            fs.Close();
            System.IO.Directory.Delete(dir);//删除文件夹

            //保存新上传的附件
            B_UserAttachment bAttachment = new B_UserAttachment();
            model.Extension = extension;
            model.SaveName = saveName;
            model.Name = fileName;
            model.VirtualPath = Path.Combine(attachmentVirtualDirectory, saveName);
            message = bAttachment.AddUserAttachment(model);

            return Json(message);
        }

        /// <summary>
        /// 根据用户Id，获取用户的营业执照附件
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBusinessLicenseByUserId(string UserId)
        {
            Message message = new Message();
            message.Success = true;
            if (string.IsNullOrEmpty(UserId))
            {
                message.Success = false;
                message.Content = "获取用户信息失败，加载数据失败";
                return Json(message);
            }

            B_UserAttachment bUserAttachment = new B_UserAttachment();
            M_UserAttachment model = new M_UserAttachment();
            model.UserId = UserId;
            model.Type = Common.UserAttachment_BusinessLicense;
            var attachments = bUserAttachment.GetUserAttachmentByMulitCondition(model);

            message.Content = JsonHandler.ToJson(attachments);

            return Json(message);
        }

        /// <summary>
        /// 根据用户Id，加载用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoadUserInfoByUserId(string UserId)
        {
            Message message = new Message();
            message.Success = true;
            if (string.IsNullOrEmpty(UserId))
            {
                message.Success = false;
                message.Content = "获取用户信息失败，加载数据失败";
                return Json(message);
            }

            B_User bUser = new B_User();
            M_User user = bUser.GetUserById(UserId);

            ///获取用户附件信息
            B_UserAttachment bUserAttachment = new B_UserAttachment();
            var attachments = bUserAttachment.GetUserAttachmentByUserId(UserId);

            user.UserAttachments = attachments;
            //营业执照附件
            user.UserBusinessLicenseAttachments = attachments.Where(a => a.Type == Common.UserAttachment_BusinessLicense).ToList();

            

            message.Content = JsonHandler.ToJson(user);

            return Json(message);
        }

        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteAttachment(string Id)
        {
            Message message = new Message();
            B_UserAttachment bAttachment = new B_UserAttachment();


            message = bAttachment.DeleteById(Id);

            return Json(message);
        }

        [HttpPost]
        public ActionResult DeleteUserBusinessAttachmentByUserId(string UserId)
        {
            Message message = new Message();

            if(string.IsNullOrEmpty(UserId))
            {
                message.Success = false;
                message.Content = "获取用户信息失败。请刷新后重新尝试";
                return Json(message);
            }

            B_UserAttachment bAttachment = new B_UserAttachment();
            M_UserAttachment model = new M_UserAttachment();
            model.UserId = UserId;
            model.Type = Common.UserAttachment_BusinessLicense;
            var attachments = bAttachment.GetUserAttachmentByMulitCondition(model);
            foreach(var attachment in attachments)
            {
               message = bAttachment.DeleteById(attachment.Id);
            }


            return Json(message);
        }
        
    }
}