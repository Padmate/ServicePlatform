using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.Service
{
    public class ProjectController:BaseController
    {
        string _imageVirtualDirectory = SystemConfig.Init.PathConfiguration["projectThumbnailsVirtualDirectory"].ToString();

        [HttpPost]
        public ActionResult GetPageData()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Project model = JsonHandler.UnJson<M_Project>(strReqStream);

            B_Project bProject = new B_Project();
            var pageData = bProject.GetPageData(model);
            var totalCount = bProject.GetPageDataTotalCount(model);

            PageResult<M_Project> pageResult = new PageResult<M_Project>(totalCount, pageData);
            return Json(pageResult);
        }


        [HttpPost]
        public ActionResult GetProjectById(string projectid)
        {
            B_Project bProject = new B_Project();

            var project = bProject.GetProjectById(projectid); ;
            return Json(project);
        }

        public ActionResult Detail(string id)
        {
            B_Project bProject = new B_Project();

            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("找不到id为空的数据信息");
            }
            var project = bProject.GetProjectById(id); ;

            ViewData["project"] = project;

            return View();
        }

        /// <summary>
        /// 上传缩略图
        /// </summary>
        /// <param name="model"></param>
        /// <param name="thumbnails"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UploadThumbnailsImage(string projectId, HttpPostedFileBase file)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片上传成功";

            //如果上传文件不为空
            if (file != null)
            {
                B_Project bProject = new B_Project();
                B_Image bImage = new B_Image();
                var project = bProject.GetProjectById(projectId);

                if (project.Image != null)
                {
                    //删除原来的图片
                    message = bImage.DeleteImage(System.Convert.ToInt32(project.Image.Id));
                    if (!message.Success) return Json(message);
                }
                //上传新图标
                message = bImage.AddImage(file, _imageVirtualDirectory, Common.Project_Thumbnails);
                if (!message.Success) return Json(message);
                int imageId = System.Convert.ToInt32(message.ReturnId);
                //更新新图标id到数据库
                message = bProject.UpdateImageId(projectId, imageId);

            }

            return Json(message);
        }

        public ActionResult Add()
        {
            return View();

        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveAdd()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Project model = JsonHandler.DeserializeJsonToObject<M_Project>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            var currentUser = this.GetCurrentUser();
            B_Project bProject = new B_Project(currentUser);
            message = bProject.AddProject(model);

            return Json(message);
        }

        public ActionResult Edit(string projectId)
        {

            B_Project bProject = new B_Project();
            var project = bProject.GetProjectById(projectId);
            ViewData["project"] = project;

            return View();
        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult SaveEdit()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Project model = JsonHandler.DeserializeJsonToObject<M_Project>(strReqStream);

            Message message = new Message();
            //校验model
            message = model.validate();
            if (!message.Success) return Json(message);

            var currentUser = this.GetCurrentUser();
            B_Project bProject = new B_Project(currentUser);
            message = bProject.EditProject(model);

            return Json(message);

        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string ProjectId)
        {
            B_Project bProject = new B_Project();
            Message message = bProject.DeleteById(ProjectId);

            return Json(message);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult BachDeleteById()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            List<string> contactIds = JsonHandler.DeserializeJsonToObject<List<string>>(strReqStream);

            Message message = new Message();
            B_Project bProject = new B_Project();

            message = bProject.BatchDeleteByIds(contactIds);
            return Json(message);
        }
    }
}