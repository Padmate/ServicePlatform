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
    public class B_ProjectDownload
    {
        D_ProjectDownload _dProjectDownload = new D_ProjectDownload();

        public M_ProjectDownload GetProjectDownloadById(string id)
        {
            var project = _dProjectDownload.GetProjectDownloadById(id);
            var result = ConverEntityToModel(project);
            return result;
        }


        public List<M_ProjectDownload> GetAllData()
        {
            var projectDownloads = _dProjectDownload.GetAll();
            var result = projectDownloads.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public List<M_ProjectDownload> GetPageData(M_ProjectDownload project)
        {
            ProjectDownload searchModel = new ProjectDownload()
            {
                ProjectId = new Guid(project.ProjectId)
            };

            var offset = project.offset;
            var limit = project.limit;

            var pageResult = _dProjectDownload.GetPageData(searchModel, offset, limit);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(M_ProjectDownload project)
        {
            ProjectDownload searchModel = new ProjectDownload()
            {
                ProjectId = new Guid(project.ProjectId)
            };

            var totalCount = _dProjectDownload.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        public M_ProjectDownload ConverEntityToModel(ProjectDownload project)
        {
            if (project == null) return null;

            B_Image bImage = new B_Image();
            var model = new M_ProjectDownload()
            {
                Id = project.Id.ToString(),
                Description = project.Description,
                VirtualPath = project.VirtualPath,
                PhysicalPath =project.PhysicalPath,
                Sequence = project.Sequence.ToString(),
                SaveName = project.SaveName,
                Extension = project.Extension,
                Image = project.ImageId == null ? null : bImage.GetImageById(System.Convert.ToInt32(project.ImageId)),

            };
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddProjectDownload(M_ProjectDownload model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "新增成功";

            try
            {
                //新增项目
                var project = new ProjectDownload()
                {
                    Description = model.Description,
                    VirtualPath = model.VirtualPath,
                    PhysicalPath = model.PhysicalPath,
                    SaveName = model.SaveName,
                    Extension = model.Extension,
                    Sequence = string.IsNullOrEmpty(model.Sequence) ? 0 : System.Convert.ToInt32(model.Sequence),
                    ProjectId = new Guid(model.ProjectId)
                };

                message.ReturnStrId = _dProjectDownload.AddProjectDownload(project);

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
        public Message EditProjectDownload(M_ProjectDownload model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "修改成功";

            try
            {
                var project = new ProjectDownload()
                {
                    Description = model.Description,
                    VirtualPath = model.VirtualPath,
                    PhysicalPath = model.PhysicalPath,
                    SaveName = model.SaveName,
                    Extension = model.Extension,
                    Sequence = string.IsNullOrEmpty(model.Sequence) ? 0 : System.Convert.ToInt32(model.Sequence)
                };

                message.ReturnStrId = _dProjectDownload.EditProjectDownload(model.Id, project);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "修改失败，异常：" + e.Message;
            }
            return message;
        }

        /// <summary>
        /// 修改文章图片id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Message UpdateImageId(string id, int imageId)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片更新成功";
            try
            {
                message.ReturnStrId = _dProjectDownload.EditImageId(id, imageId);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "图片更新失败:" + e.Message;
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
                //删除图片文件
                var projectDownload = _dProjectDownload.GetProjectDownloadById(id);
                if (!string.IsNullOrEmpty(projectDownload.VirtualPath))
                {
                    var physicalPath = HttpContext.Current.Server.MapPath("~" + projectDownload.VirtualPath);

                    if (System.IO.File.Exists(physicalPath))
                        System.IO.File.Delete(physicalPath);
                }

                _dProjectDownload.DeleteProjectDownload(id);

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
                _dProjectDownload.BatchDeleteProjectDownload(ids);

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
        public Message AddFile(HttpPostedFileBase file, string virtualDirectory,M_ProjectDownload model)
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


                    var attachment = new ProjectDownload()
                    {
                        VirtualPath = virtualPath,
                        Description = model.Description,
                        Extension = extension,
                        SaveName = saveName,
                        Sequence = System.Convert.ToInt32(model.Sequence),
                        ProjectId = new Guid(model.ProjectId)
                    };
                    message.ReturnStrId = _dProjectDownload.AddProjectDownload(attachment);
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
