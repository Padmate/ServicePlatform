using Padmate.ServicePlatform.DataAccess;
using Padmate.ServicePlatform.Entities;
using Padmate.ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Padmate.ServicePlatform.Service
{
    public class B_UserAttachment
    {
        D_UserAttachment _attachment = new D_UserAttachment();

        public M_UserAttachment GetUserAttachmentById(string id)
        {
            var intId = System.Convert.ToInt32(id);
            var attachment = _attachment.GetUserAttachmentById(intId);
            var result = ConverEntityToModel(attachment);
            return result;
        }

        public List<M_UserAttachment> GetUserAttachmentByUserId(string userId)
        {
            var attachments = _attachment.GetUserAttachmentByUserId(userId);
            var result = attachments.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }


        public List<M_UserAttachment> GetUserAttachmentByMulitCondition(M_UserAttachment model)
        {
            UserAttachment searchModel = new UserAttachment()
            {
                UserId = model.UserId,
                Type = model.Type
            };
            var attachments = _attachment.GetBuMulitCondition(searchModel);
            var result = attachments.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        public List<M_UserAttachment> GetAllData()
        {
            var attachments = _attachment.GetAll();
            var result = attachments.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public List<M_UserAttachment> GetPageData(M_UserAttachment attachment)
        {
            UserAttachment searchModel = new UserAttachment()
            {
                UserId =attachment.UserId
            };

            var offset = attachment.offset;
            var limit = attachment.limit;

            var pageResult = _attachment.GetPageData(searchModel, offset, limit);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(M_UserAttachment attachment)
        {
            UserAttachment searchModel = new UserAttachment()
            {
                UserId = attachment.UserId
            };

            var totalCount = _attachment.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        public M_UserAttachment ConverEntityToModel(UserAttachment attachment)
        {
            if (attachment == null) return null;

            var model = new M_UserAttachment()
            {
                Id = attachment.Id.ToString(),
                VirtualPath = attachment.VirtualPath,
                PhysicalPath = attachment.PhysicalPath,
                Name = attachment.Name,
                SaveName = attachment.SaveName,
                Extension = attachment.Extension,
                Type = attachment.Type

            };
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddUserAttachment(M_UserAttachment model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "新增成功";

            try
            {
                //新增项目
                var attachment = new UserAttachment()
                {
                    VirtualPath = model.VirtualPath,
                    PhysicalPath = model.PhysicalPath,
                    SaveName = model.SaveName,
                    Extension = model.Extension,
                    Name = model.Name,
                    Type = model.Type,
                    UserId = model.UserId
                };

                message.ReturnId = _attachment.AddUserAttachment(attachment);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "新增失败，异常：" + e.Message;
            }
            return message;
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Message EditUserAttachment(M_UserAttachment model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "修改成功";

            try
            {
                var attachment = new UserAttachment()
                {

                    VirtualPath = model.VirtualPath,
                    PhysicalPath = model.PhysicalPath,
                    SaveName = model.SaveName,
                    Extension = model.Extension,
                    Type = model.Type,
                    Name = model.Name
                };

                var id = System.Convert.ToInt32(model.Id);
                message.ReturnId = _attachment.EditUserAttachment(id, attachment);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "修改失败，异常：" + e.Message;
            }
            return message;
        }



        public Message DeleteById(string id)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "附件删除成功";

            try
            {
                var intId = System.Convert.ToInt32(id);
                //删除文件
                var attachment = _attachment.GetUserAttachmentById(intId);
                if (!string.IsNullOrEmpty(attachment.VirtualPath))
                {
                    var physicalPath = HttpContext.Current.Server.MapPath("~" + attachment.VirtualPath);

                    if (System.IO.File.Exists(physicalPath))
                        System.IO.File.Delete(physicalPath);
                }

                _attachment.DeleteUserAttachment(intId);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "附件删除失败，异常：" + e.Message;
            }
            return message;
        }

        public Message BatchDeleteByIds(List<string> ids)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "删除成功";

            try
            {
                //删除文件
                // _attachment.BatchDeleteUserAttachment(ids);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "删除失败：" + e.Message;
            }

            return message;
        }


        /// <summary>
        /// 新增附件
        /// </summary>
        /// <returns></returns>
        public Message AddFile(HttpPostedFileBase file, string virtualDirectory, M_UserAttachment model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "附件新增成功";
            try
            {
                if (file != null)
                {
                    #region
                    //保存附件
                    Message fileMsg = SaveFile(file, virtualDirectory);
                    if (!fileMsg.Success)
                        return message;
                    FileInfo imageInfo = new FileInfo(file.FileName);
                    var saveName = fileMsg.Content;
                    var fileName = imageInfo.Name;
                    var extension = imageInfo.Extension;
                    var virtualPath = Path.Combine(virtualDirectory, saveName);


                    var attachment = new UserAttachment()
                    {
                        VirtualPath = virtualPath,
                        Extension = extension,
                        SaveName = saveName,
                        Name = fileName,
                        Type = model.Type,
                        UserId = model.UserId
                    };
                    message.ReturnId = _attachment.AddUserAttachment(attachment);
                    #endregion
                }
            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "附件新增失败。异常：" + e.Message + e.InnerException + e;
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
    }
}
