using Padmate.ServicePlatform.DataAccess;
using Padmate.ServicePlatform.Entities;
using Padmate.ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Service
{
    public class B_Project
    {
        D_Project _dProject = new D_Project();

        public B_Project()
        {

        }

        M_User _currentUser;
        public B_Project(M_User currentUser)
        {
            _currentUser = currentUser;

        }


        public M_Project GetProjectById(string id)
        {
            var project = _dProject.GetProjectById(id);
            var result = ConverEntityToModel(project);
            return result;
        }

        public List<M_Project> GetProjectByType(string type)
        {
            List<M_Project> result = new List<M_Project>();
            if(!string.IsNullOrEmpty(type))
            {
                var projects = _dProject.GetProjectByType(type);
                result = projects.Select(a => ConverEntityToModel(a)).ToList();


            }
            return result;
        }

        public List<M_Project> GetAllData()
        {
            var projects = _dProject.GetAll();
            var result = projects.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public List<M_Project> GetPageData(M_Project project)
        {
            Project searchModel = new Project()
            {
                Name = project.Name
            };

            var offset = project.offset;
            var limit = project.limit;


            var pageResult = _dProject.GetPageData(searchModel, offset, limit);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(M_Project project)
        {
            Project searchModel = new Project()
            {
                Name = project.Name
            };

            var totalCount = _dProject.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        private M_Project ConverEntityToModel(Project project)
        {
            if (project == null) return null;

             B_Image bImage = new B_Image();
            var model = new M_Project()
            {
                Id = project.Id.ToString(),
                Name = project.Name,
                Description = project.Description,
                Content = project.Content,
                Sequence = project.Sequence.ToString(),
                Creator = project.Creator,
                CreateDate = project.CreateDate,
                Modifier = project.Modifier,
                ModifiedDate = project.ModifiedDate,
                Type = project.Type,
                Image = project.ImageId == null ? null : bImage.GetImageById(System.Convert.ToInt32(project.ImageId)),
                //ProjectDownloads = project.ProjectDownloads

            };
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddProject(M_Project model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "项目新增成功";

            try
            {
                //新增项目
                var project = new Project()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Content = model.Content,
                    CreateDate = DateTime.Now,
                    Creator = _currentUser.UserName,
                    Type = model.Type,
                    Sequence = string.IsNullOrEmpty(model.Sequence) ? 0 : System.Convert.ToInt32(model.Sequence)
                    
                };

                message.ReturnStrId = _dProject.AddProject(project);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "项目新增失败，异常：" + e.Message;
            }
            return message;
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Message EditProject(M_Project model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "项目修改成功";

            try
            {
                var project = new Project()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Content = model.Content,
                    CreateDate = DateTime.Now,
                    Creator = _currentUser.UserName,
                    Type = model.Type,
                    Sequence = string.IsNullOrEmpty(model.Sequence) ? 0 : System.Convert.ToInt32(model.Sequence)
                };

                message.ReturnStrId = _dProject.EditProject(model.Id, project);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "项目修改失败，异常：" + e.Message;
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
                message.ReturnStrId = _dProject.EditImageId(id, imageId);

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
            message.Content = "项目删除成功";

            try
            {
                _dProject.DeleteProject(id);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "项目删除失败：" + e.Message;
            }

            return message;
        }

        public Message BatchDeleteByIds(List<string> ids)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "项目删除成功";

            try
            {
                _dProject.BatchDeleteProject(ids);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "项目删除失败：" + e.Message;
            }

            return message;
        }
    }
}
