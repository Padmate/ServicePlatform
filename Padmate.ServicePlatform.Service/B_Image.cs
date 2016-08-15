using Padmate.ServicePlatform.DataAccess;
using Padmate.ServicePlatform.Entities;
using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Padmate.ServicePlatform.Service
{
    public class B_Image
    {
        D_Image _dImage = new D_Image();
        
        /// <summary>
        /// 获取首页背景图片
        /// </summary>
        /// <returns></returns>
        public List<M_Image> GetHomeBGImages()
        {
            var images = _dImage.GetImagesByType(Common.Image_HomeBG);
            var result = images.Select(i=>new M_Image(){
                Id = i.Id,
                VirtualPath = i.VirtualPath,
                PhysicalPath = i.PhysicalPath,
                Name = i.Name,
                SaveName = i.SaveName,
                Extension = i.Extension,
                Sequence = i.Sequence,
                Type = i.Type

            }).ToList();

            return result;
        }

        /// <summary>
        /// 根据ID获取图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public M_Image GetImageById(int id)
        {
            var image = _dImage.GetImageById(id);
            if (image == null) return null;
            var result = new M_Image()
            {
                Id = image.Id,
                VirtualPath = image.VirtualPath,
                PhysicalPath = image.PhysicalPath,
                Name = image.Name,
                SaveName = image.SaveName,
                Extension = image.Extension,
                Sequence = image.Sequence,
                Type = image.Type

            };

            return result;
        }

        

        /// <summary>
        /// 新增图片
        /// </summary>
        /// <returns></returns>
        public Message AddImage(HttpPostedFileBase file,string virtualDirectory,string imageType)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片新增成功";
            try
            {
                if (file != null)
                {
                    #region
                    //保存缩略图
                    Message imgMsg = SaveFile(file, virtualDirectory);
                    if (!imgMsg.Success)
                        return message;
                    FileInfo imageInfo = new FileInfo(file.FileName);
                    var saveName = imgMsg.Content;
                    var fileName = imageInfo.Name;
                    var extension = imageInfo.Extension;
                    var virtualPath = Path.Combine(virtualDirectory, saveName);

                    //图片顺序
                    B_Image _bImage = new B_Image();
                    var totalImages = _dImage.GetImagesByType(imageType);
                    var sequence = totalImages.Count + 1;

                    var image = new Image()
                    {
                        VirtualPath = virtualPath,
                        Name = fileName,
                        Extension = extension,
                        SaveName = saveName,
                        Sequence = sequence,
                        Type = imageType
                    };
                    message.ReturnId = _dImage.AddImage(image);
                    #endregion
                }
            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "图片新增失败。异常：" + e.Message + e.InnerException + e;
            }
            return message;
        }


        /// <summary>
        /// 保存文件
        /// Success:true,则返回文件保存名称
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private Message SaveFile(HttpPostedFileBase file, string virtualDirectory)
        {
            Message message = new Message();
            message.Success = true;
            try
            {
                FileInfo fileInfo = new FileInfo(file.FileName);
                var fileName = fileInfo.Name;
                var extension = fileInfo.Extension;
                string saveName = Guid.NewGuid().ToString() + extension;

                string physicleDirectory = HttpContext.Current.Server.MapPath("~" + virtualDirectory);
                if (!System.IO.Directory.Exists(physicleDirectory))
                {
                    System.IO.Directory.CreateDirectory(physicleDirectory);
                }
                string physicalPath = Path.Combine(physicleDirectory, saveName);
                file.SaveAs(physicalPath);

                message.Content = saveName;
            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "文件保存失败。异常：" + e.Message;
            }
            return message;
        }

        /// <summary>
        /// 更新图片顺序
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public Message UpdateImageSequence(int id,int sequence)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片顺序更新成功";

            try
            {
                _dImage.UpdateImageSequence(id, sequence);
                
            }catch(Exception e)
            {
                message.Success = false;
                message.Content = "图片顺序更新失败，异常："+e.Message ;
            }
            return message;
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message DeleteImage(int id)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片删除成功";
            try
            {
                //删除图片文件
                var image = _dImage.GetImageById(id);
                if (!string.IsNullOrEmpty(image.VirtualPath))
                {
                    var physicalPath = HttpContext.Current.Server.MapPath("~" + image.VirtualPath);

                    if (System.IO.File.Exists(physicalPath))
                        System.IO.File.Delete(physicalPath);
                }

                _dImage.DeleteImage(id);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "图片删除失败，异常：" + e.Message;
            }
            return message;

        }

        
    }
}
