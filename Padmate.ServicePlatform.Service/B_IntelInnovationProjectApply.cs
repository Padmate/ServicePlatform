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
    public class B_IntelInnovationProjectApply
    {
        D_IntelInnovationProjectApply _dProject = new D_IntelInnovationProjectApply();

        public B_IntelInnovationProjectApply()
        {

        }


        public M_IntelInnovationProjectApply GetIntelInnovationProjectApplyById(string id)
        {
            var intId = System.Convert.ToInt32(id);
            var project = _dProject.GetIntelInnovationProjectApplyById(intId);
            var result = ConverEntityToModel(project);
            return result;
        }

        public List<M_IntelInnovationProjectApply> GetIntelInnovationProjectApplyByUserId(string userId)
        {
            List<M_IntelInnovationProjectApply> result = new List<M_IntelInnovationProjectApply>();
            if (!string.IsNullOrEmpty(userId))
            {
                var projects = _dProject.GetIntelInnovationProjectApplyByUserId(userId);
                result = projects.Select(a => ConverEntityToModel(a)).ToList();


            }
            return result;
        }

        public List<M_IntelInnovationProjectApply> GetAllData()
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
        public List<M_IntelInnovationProjectApply> GetPageData(M_IntelInnovationProjectApply project)
        {
            IntelInnovationProjectApply searchModel = new IntelInnovationProjectApply()
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
        public int GetPageDataTotalCount(M_IntelInnovationProjectApply project)
        {
            IntelInnovationProjectApply searchModel = new IntelInnovationProjectApply()
            {
                Name = project.Name
            };

            var totalCount = _dProject.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        private M_IntelInnovationProjectApply ConverEntityToModel(IntelInnovationProjectApply project)
        {
            if (project == null) return null;

            B_Image bImage = new B_Image();
            B_IntelInnovationProjectApplyAttachment bAttachment = new B_IntelInnovationProjectApplyAttachment();
            B_IntelInnovationProjectApplyQue bQue = new B_IntelInnovationProjectApplyQue();

            var model = new M_IntelInnovationProjectApply()
            {
                Id = project.Id.ToString(),
                UserId = project.UserId,
                Name = string.IsNullOrEmpty(project.Name)?string.Empty :project.Name,
                Description = string.IsNullOrEmpty(project.Description) ? string.Empty : project.Description,
                HasExample = project.HasExample,
                InnovationPoint = string.IsNullOrEmpty(project.InnovationPoint) ? string.Empty : project.InnovationPoint,
                Contact = string.IsNullOrEmpty(project.Contact) ? string.Empty : project.Contact,
                ContactPhone = string.IsNullOrEmpty(project.ContactPhone) ? string.Empty : project.ContactPhone,
                Attachments = project.Attachments.Select(p => bAttachment.ConverEntityToModel(p)).ToList(),
                Ques = project.Ques.Select(p => bQue.ConverEntityToModel(p)).ToList()

            };
            return model;
        }

        /// <summary>
        /// 新增空白数据
        /// </summary>
        /// <returns></returns>
        public Message AddEmptyData(string userid)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "项目新增成功";

            if(string.IsNullOrEmpty(userid))
            {
                message.Success = false;
                message.Content = "获取用户信息失败，数据新增失败";
                return message;
            }
            M_IntelInnovationProjectApply emptyModel = new M_IntelInnovationProjectApply()
            {
                UserId = userid
            };
            message = this.AddIntelInnovationProjectApply(emptyModel);
            return message;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddIntelInnovationProjectApply(M_IntelInnovationProjectApply model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "项目新增成功";

            try
            {
                //新增项目
                var project = new IntelInnovationProjectApply()
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    Description = model.Description,
                    HasExample = model.HasExample,
                    ContactPhone = model.ContactPhone,
                    Contact = model.Contact,
                    InnovationPoint = model.InnovationPoint
                    
                };

                message.ReturnStrId = _dProject.AddIntelInnovationProjectApply(project);

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
        public Message EditIntelInnovationProjectApply(M_IntelInnovationProjectApply model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "项目修改成功";

            try
            {
                var project = new IntelInnovationProjectApply()
                {
                    Name = model.Name,
                    Description = model.Description,
                    HasExample = model.HasExample,
                    ContactPhone = model.ContactPhone,
                    Contact = model.Contact,
                    InnovationPoint = model.InnovationPoint

                };
                var id = System.Convert.ToInt32(model.Id);
                message.ReturnId = _dProject.EditIntelInnovationProjectApply(id, project);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "项目修改失败，异常：" + e.Message;
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
                _dProject.DeleteIntelInnovationProjectApply(id);

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
                _dProject.BatchDeleteIntelInnovationProjectApply(ids);

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
