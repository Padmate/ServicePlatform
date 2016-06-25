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

namespace Padmate.ServicePlatform.Service
{
    public class B_Image
    {
        D_Image _dImage = new D_Image();
        public B_Image()
        {

        }

        string _mapPath;
        public B_Image(string mapPath)
        {
            _mapPath = mapPath;
        }

        /// <summary>
        /// 获取首页背景图片
        /// </summary>
        /// <returns></returns>
        public List<M_Image> GetHomeBGImages()
        {
            var images = _dImage.GetImagesByType(Common.Image_HomeBG);
            var result = images.Select(i=>new M_Image(){
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                Sequence = i.Sequence,
                Type = i.Type

            }).ToList();

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
                model.ImageUrl = image.ImageUrl;
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
                Image image = _dImage.GetImageById(id);
                if (image != null)
                {
                    //删除原来图片
                    if (!string.IsNullOrEmpty(image.ImageUrl))
                    {
                        var physicalPath = Path.Combine(_mapPath,image.ImageUrl);
                        if (System.IO.File.Exists(physicalPath))
                            System.IO.File.Delete(physicalPath);
                    }

                    _dImage.DeleteImage(id);
                }

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "图片顺序更新失败，异常：" + e.Message;
            }
            return message;

        }

        
    }
}
