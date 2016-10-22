using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.Infra
{
    public class ModuleController:BaseController
    {
        string _imageVirtualDirectory = SystemConfig.Init.PathConfiguration["moduleThumbnailsVirtualDirectory"].ToString();


        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="page">当前所在页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPageData()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Module model = JsonHandler.UnJson<M_Module>(strReqStream);

            B_Module bModule = new B_Module();
            var pageData = bModule.GetPageData(model);
            var totalCount = bModule.GetPageDataTotalCount(model);

            PageResult<M_Module> pageResult = new PageResult<M_Module>(totalCount, pageData);
            return Json(pageResult);
        }

        [HttpPost]
        public ActionResult GetModuleById(int moduleid)
        {
            B_Module bModule = new B_Module();

            var module = bModule.GetModuleById(moduleid); ;
            return Json(module);
        }

        public ActionResult Detail(int moduleId)
        {
            B_Module bModule = new B_Module();


            var module = bModule.GetModuleById(moduleId); ;

            ViewData["module"] = module;

            return View();
        }

        public ActionResult ShowDetail(string urlId)
        {
            B_Module bModule = new B_Module();


            var module = bModule.GetModuleByUrlId(urlId); ;

            ViewData["module"] = module;

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
        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
        public ActionResult UploadThumbnailsImage(int moduleId, HttpPostedFileBase file)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片上传成功";

            //如果上传文件不为空
            if (file != null)
            {
                B_Module bModule = new B_Module();
                B_Image bImage = new B_Image();
                var module = bModule.GetModuleById(moduleId);

                if (module.Image != null)
                {
                    //删除原来的图片
                    message = bImage.DeleteImage(System.Convert.ToInt32(module.Image.Id));
                    if (!message.Success) return Json(message);
                }
                //上传新图标
                message = bImage.AddImage(file, _imageVirtualDirectory, Common.Module_Thumbnails);
                if (!message.Success) return Json(message);
                int imageId = System.Convert.ToInt32(message.ReturnId);
                //更新新图标id到数据库
                message = bModule.UpdateImageId(moduleId, imageId);

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
        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
        public ActionResult SaveAdd()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Module model = JsonHandler.DeserializeJsonToObject<M_Module>(strReqStream);

            Message message = new Message();
            //校验model
            message = ValideData(model);
            if (!message.Success) return Json(message);

            var currentUser = this.GetCurrentUser();
            B_Module bModule = new B_Module(currentUser);
            message = bModule.AddModule(model);

            return Json(message);
        }

        public ActionResult Edit(int moduleId)
        {

            B_Module bModule = new B_Module();
            var module = bModule.GetModuleById(moduleId);
            ViewData["module"] = module;

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
            M_Module model = JsonHandler.DeserializeJsonToObject<M_Module>(strReqStream);

            Message message = new Message();
            //校验model
            message = ValideData(model);
            if (!message.Success) return Json(message);

            var currentUser = this.GetCurrentUser();
            B_Module bModule = new B_Module(currentUser);
            message = bModule.EditModule(model);

            return Json(message);

        }


        private Message ValideData(M_Module model)
        {
            Message message = new Message();
            message.Success = true;

            message = model.validate();
            if (!message.Success) return message;

            if (model.IsHref && string.IsNullOrEmpty(model.Href))
            {
                message.Success = false;
                message.Content = "模块链接不能为空";
                return message;
            }
            return message;
        }

        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
        public ActionResult Delete(int ModuleId)
        {
            B_Module bModule = new B_Module();
            Message message = bModule.DeleteModule(ModuleId);

            return Json(message);
        }

        [HttpPost]
        [Authorize(Roles = SystemRole.SystemAdmin + "," + SystemRole.BackstageAdmin)]
        public ActionResult BachDeleteById()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            List<int> moduleIds = JsonHandler.DeserializeJsonToObject<List<int>>(strReqStream);

           
            Message message = new Message();
            B_Module bModule = new B_Module();
            message = bModule.BatchDeleteByIds(moduleIds);
            return Json(message);
        }

        public ActionResult GenerateNewURLId()
        {
            Message message = new Message();
            message.Success = true;

            try
            {
                message.ReturnStrId = Guid.NewGuid().ToString();


            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "生成唯一标识失败：" + e.Message;
            }

            return Json(message);
        }
    }
}