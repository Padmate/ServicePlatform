using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers
{
    public class ImageController:BaseController
    {
        #region 背景图片管理

        //允许的图片类型
        public List<string> AllowImageExtensions = new List<string>() { ".gif", ".jpg", ".jpeg", ".bmp", ".png" };
        //背景图片虚拟目录
        private string homebgVirtualDirectory = SystemConfig.Init.PathConfiguration["homebgVirtualDirectory"].ToString();
       
        /// <summary>
        /// 获取背景图片
        /// </summary>
        /// <returns></returns>
        public ActionResult GetHomeBGImages()
        {
            B_Image _bImage = new B_Image();
            var bgImages = _bImage.GetHomeBGImages();

            return Json(bgImages);

        }

        /// <summary>
        /// 更新图片顺序
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateImageSequence()
        {

            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            List<M_Image> images = JsonHandler.DeserializeJsonToObject<List<M_Image>>(strReqStream);

            B_Image _bImage = new B_Image();
            if (images != null)
            {
                foreach (var image in images)
                {
                    Message message = _bImage.UpdateImageSequence(image.Id, image.Sequence);
                }
            }

            return Json(true);
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteImage(int Id)
        {
            Message message = new Message();
            B_Image bImage = new B_Image();

            M_Image image = bImage.GetImageById(Id);
            if (image != null)
            {
                //删除图片
                if (!string.IsNullOrEmpty(image.VirtualPath))
                {
                    var physicalPath = HttpContext.Server.MapPath("~"+image.VirtualPath);

                    if (System.IO.File.Exists(physicalPath))
                        System.IO.File.Delete(physicalPath);
                }
            }


            message = bImage.DeleteImage(Id);

            return Json(message);
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="model"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UploadHomeBGImage(HttpPostedFileBase file)
        {
            Message message = new Message();
            message.Success = true;

            try
            {
                if (file != null)
                {
                    #region 保存图片文件
                    FileInfo imageInfo = new FileInfo(file.FileName);
                    var fileName = imageInfo.Name;
                    var extension = imageInfo.Extension;
                    string saveName = Guid.NewGuid().ToString() + imageInfo.Extension;
                    string virtualPath = Path.Combine(homebgVirtualDirectory, saveName);

                    string physicleDirectory = Server.MapPath(homebgVirtualDirectory);
                    if (!System.IO.Directory.Exists(physicleDirectory))
                    {
                        System.IO.Directory.CreateDirectory(physicleDirectory);
                    }
                    string physicalPath = Path.Combine(physicleDirectory, saveName);
                    file.SaveAs(physicalPath);
                    #endregion

                    #region 保存图片信息
                    //图片顺序
                    B_Image _bImage = new B_Image();
                    var totalImages = _bImage.GetHomeBGImages();
                    var sequence = totalImages.Count + 1;

                    var imageModel = new M_Image()
                    {
                        VirtualPath = virtualPath,
                        Name = fileName,
                        Extension = extension,
                        SaveName = saveName,
                        Sequence = sequence,
                        Type = Common.Image_HomeBG
                    };
                    message = _bImage.AddImage(imageModel);
                    if (!message.Success)
                        return Json(message);
                    
                    #endregion

                }
                else
                {
                    message.Success = false;
                    message.Content = "上传失败。未获取到图片信息";
                }

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "上传失败。异常：" + e.Message + e.InnerException + e;
            }

            return Json(message);

        }

        #endregion
    }
}