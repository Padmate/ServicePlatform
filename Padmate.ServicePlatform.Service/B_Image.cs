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
        public Message AddImage(M_Image image)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片新增成功";
            try
            {
                Image model = new Image();
                model.VirtualPath = image.VirtualPath;
                model.PhysicalPath = image.PhysicalPath;
                model.Name = image.Name;
                model.SaveName = image.SaveName;
                model.Extension = image.Extension;
                model.Sequence = image.Sequence;
                model.Type = image.Type;
                _dImage.AddImage(model);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "图片新增失败。异常：" + e.Message + e.InnerException + e;
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
