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
    public class ImageController:BaseController
    {
        #region 背景图片管理

        public List<string> AllowImageExtensions = new List<string>() { ".gif", ".jpg", ".jpeg", ".bmp", ".png" };
        private string homebgVirtualFloader = "../img/Upload/homebg/";
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
        /// 
        /// </summary>
        /// <param name="images"></param>
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

        [HttpPost]
        public ActionResult DeleteImage(int Id)
        {
            Message message = new Message();

            B_Image bImage = new B_Image(Server.MapPath(""));
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
        public ActionResult UploadHomeBGImage(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string virtualUrl = "";
            Message message = new Message();
            message.Success = true;

            try
            {
                if (file != null)
                {
                    FileInfo articleImageFile = new FileInfo(file.FileName);
                    string saveName = Guid.NewGuid().ToString() + articleImageFile.Extension;
                    virtualUrl = Path.Combine(homebgVirtualFloader, saveName);
                    string physicleDirectory = Server.MapPath(homebgVirtualFloader);
                    string physicalUrl = Path.Combine(physicleDirectory, saveName);
                    if (!System.IO.Directory.Exists(physicleDirectory))
                    {
                        System.IO.Directory.CreateDirectory(physicleDirectory);
                    }
                    file.SaveAs(physicalUrl);

                }
                //图片顺序
                B_Image _bImage = new B_Image();
                var totalImages = _bImage.GetHomeBGImages();
                var sequence = totalImages.Count + 1;

                var imageModel = new M_Image()
                {
                    ImageUrl = virtualUrl,
                    Sequence = sequence,
                    Type = Common.Image_HomeBG
                };
                message = _bImage.AddImage(imageModel);
                if (!message.Success)
                {
                    return Json(message);
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