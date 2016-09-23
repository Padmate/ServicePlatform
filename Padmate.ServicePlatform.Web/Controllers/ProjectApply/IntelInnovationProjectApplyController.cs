using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.ProjectApply
{
    [Authorize]
    public class IntelInnovationProjectApplyController:BaseController
    {

        public ActionResult Index()
        {
            //获取当前登录用户
            var loginUser = this.GetCurrentUser();
            B_User bUser = new B_User();
            var user = bUser.GetUserByName(loginUser.UserName);
            ViewData["UserInfo"] = user;


            //根据用户ID查找该用户下的IntelInnovationProjectApply数据，如果找不到，则新增一条空白数据
            B_IntelInnovationProjectApply bProject = new B_IntelInnovationProjectApply();
            var existProjects = bProject.GetIntelInnovationProjectApplyByUserId(user.Id);
            if (existProjects.Count == 0)
            {
                Message message = bProject.AddEmptyData(user.Id);
                if (!message.Success)
                {
                    throw new Exception(message.Content);
                }

            }

            //重新查找数据
            var projects = bProject.GetIntelInnovationProjectApplyByUserId(user.Id);
            var project = projects.First();
            //找出最新的队列
            if (project.Ques != null && project.Ques.Count > 0)
            {
                project.LatestQue = project.Ques.OrderByDescending(q => q.ApplicationDate).First();
            }
            ViewData["project"] = project;

            //审核状态
            ViewData["AuditStatus"] = JsonHandler.ToJson(Common.Dic_Audit);

            return View();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveEdit()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_IntelInnovationProjectApply model = JsonHandler.DeserializeJsonToObject<M_IntelInnovationProjectApply>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            var currentUser = this.GetCurrentUser();
            B_IntelInnovationProjectApply bIntelInnovationProjectApply = new B_IntelInnovationProjectApply();
            message = bIntelInnovationProjectApply.EditIntelInnovationProjectApply(model);
            

            if (message.Success) message.Content = "保存成功";
            return Json(message);

        }

        /// <summary>
        /// 提交申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Apply()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_IntelInnovationProjectApply model = JsonHandler.DeserializeJsonToObject<M_IntelInnovationProjectApply>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            var currentUser = this.GetCurrentUser();
            B_IntelInnovationProjectApply bIntelInnovationProjectApply = new B_IntelInnovationProjectApply();
            message = bIntelInnovationProjectApply.EditIntelInnovationProjectApply(model);
            //新增一条提交申请的队列
            if(message.Success)
            {
                var projectId = message.ReturnId;
                B_IntelInnovationProjectApplyQue bQue = new B_IntelInnovationProjectApplyQue();
                M_IntelInnovationProjectApplyQue mQue = new M_IntelInnovationProjectApplyQue()
                {
                    IntelInnovationProjectApplyId = projectId.ToString(),
                    AuditStatus = Common.Audit_Waiting,  //等待审核
                    Application = currentUser.UserName,
                    ApplicationDate = DateTime.Now
                };

                message = bQue.AddIntelInnovationProjectApplyQue(mQue);
            }

            if (message.Success) message.Content = "申请成功";
            return Json(message);

        }

        [HttpPost]
        public ActionResult LoadProjectDataByUserId(string UserId)
        {
            Message message = new Message();
            message.Success = true;
            if(string.IsNullOrEmpty(UserId))
            {
                message.Success = false;
                message.Content = "获取用户信息失败，加载项目数据失败";
                return Json(message);
            }
            B_IntelInnovationProjectApply bProject = new B_IntelInnovationProjectApply();
            var projects = bProject.GetIntelInnovationProjectApplyByUserId(UserId);
            if(projects.Count == 0)
            {
                message.Success = false;
                message.Content = "加载项目数据失败";
                return Json(message);
            }

            var project = projects.First();
            //找出最新的队列
            if(project.Ques != null && project.Ques.Count >0)
            {
                project.LatestQue = project.Ques.OrderByDescending(q => q.ApplicationDate).First();
            }

            message.Content = JsonHandler.ToJson(projects.First());

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
            string attachmentVirtualDirectory = SystemConfig.Init.PathConfiguration["intelProjectAttachmentVirtualDirectory"].ToString();

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
        public ActionResult MergeFile(M_IntelInnovationProjectApplyAttachment model)
        {
            Message message = new Message();
            message.Success = true;

            string attachmentVirtualDirectory = SystemConfig.Init.PathConfiguration["intelProjectAttachmentVirtualDirectory"].ToString();

            #region 校验
            if (string.IsNullOrEmpty(model.IntelInnovationProjectApplyId))
            {
                message.Success = false;
                message.Content = "未获取项目ID，请重新尝试";
                return Json(message);
            }

            message = model.validate();
            if (!message.Success) return Json(message);
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
                if(System.IO.File.Exists(part))
                {
                    var bytes = System.IO.File.ReadAllBytes(part);
                    fs.Write(bytes, 0, bytes.Length);
                    bytes = null;
                    System.IO.File.Delete(part);//删除分块
                }
                
            }
            fs.Close();
            System.IO.Directory.Delete(dir);//删除文件夹

            B_IntelInnovationProjectApplyAttachment bAttachment = new B_IntelInnovationProjectApplyAttachment();
            model.Extension = extension;
            model.SaveName = saveName;
            model.Name = fileName;
            model.VirtualPath = Path.Combine(attachmentVirtualDirectory, saveName);
            message = bAttachment.AddIntelInnovationProjectApplyAttachment(model);

            return Json(message);
        }

        [HttpPost]
        public ActionResult LoadProjectAttachmentDataByProjectId(string ProjectId)
        {
            B_IntelInnovationProjectApplyAttachment bAttachment = new B_IntelInnovationProjectApplyAttachment();
            var attachments = bAttachment.GetIntelInnovationProjectApplyAttachmentByProjectId(ProjectId);

            return Json(attachments);
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
            B_IntelInnovationProjectApplyAttachment bAttachment = new B_IntelInnovationProjectApplyAttachment();


            message = bAttachment.DeleteById(Id);

            return Json(message);
        }
    }
}