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
        M_User _currentUser;
        public B_IntelInnovationProjectApply(M_User currentUser)
        {
            _currentUser = currentUser;

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
        /// 获取分页数据
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public List<M_IntelInnovationProjectApplySearch> GetPageDataForAudit(M_IntelInnovationProjectApplySearch project)
        {
            IntelInnovationProjectApplySearch searchModel = new IntelInnovationProjectApplySearch()
            {
                Name = project.Name,
                AuditStatus = project.AuditStatus
            };

            var offset = project.offset;
            var limit = project.limit;


            var pageResult = _dProject.GetPageDataForAudit(searchModel, offset, limit);
            var result = pageResult.Select(a => ConverSearchEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCountForAudit(M_IntelInnovationProjectApplySearch project)
        {
            IntelInnovationProjectApplySearch searchModel = new IntelInnovationProjectApplySearch()
            {
                Name = project.Name,
                AuditStatus = project.AuditStatus
            };

            var totalCount = _dProject.GetPageDataTotalCountForAudit(searchModel);
            return totalCount;
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
                OrganizationName = string.IsNullOrEmpty(project.OrganizationName) ? string.Empty : project.OrganizationName,
                FieldScopeCode = string.IsNullOrEmpty(project.FieldScopeCode) ? string.Empty : project.FieldScopeCode,
                FieldScopeName = string.IsNullOrEmpty(project.FieldScopeName) ? string.Empty : project.FieldScopeName,
                ProjectStage = string.IsNullOrEmpty(project.ProjectStage) ? string.Empty : project.ProjectStage,
                BusinessLicense = string.IsNullOrEmpty(project.BusinessLicense) ? string.Empty : project.BusinessLicense,
                FoundedTime = project.FoundedTime,
                BusinessAddress = string.IsNullOrEmpty(project.BusinessAddress) ? string.Empty : project.BusinessAddress,
                Website = string.IsNullOrEmpty(project.Website) ? string.Empty : project.Website,
                WebChatNumber = string.IsNullOrEmpty(project.WebChatNumber) ? string.Empty : project.WebChatNumber,
                Principal = string.IsNullOrEmpty(project.Principal) ? string.Empty : project.Principal,
                PrincipalPosition = string.IsNullOrEmpty(project.PrincipalPosition) ? string.Empty : project.PrincipalPosition,
                PrincipalPhone = string.IsNullOrEmpty(project.PrincipalPhone) ? string.Empty : project.PrincipalPhone,
                PrincipalMail = string.IsNullOrEmpty(project.PrincipalMail) ? string.Empty : project.PrincipalMail,
                Contact = string.IsNullOrEmpty(project.Contact) ? string.Empty : project.Contact,
                ContactPosition = string.IsNullOrEmpty(project.ContactPosition) ? string.Empty : project.ContactPosition,
                ContactPhone = string.IsNullOrEmpty(project.ContactPhone) ? string.Empty : project.ContactPhone,
                ContactMail = string.IsNullOrEmpty(project.ContactMail) ? string.Empty : project.ContactMail,
                OrganizationDescription = string.IsNullOrEmpty(project.OrganizationDescription) ? string.Empty : project.OrganizationDescription,
                CoreTechnology = string.IsNullOrEmpty(project.CoreTechnology) ? string.Empty : project.CoreTechnology,
                Keyword = string.IsNullOrEmpty(project.Keyword) ? string.Empty : project.Keyword,

                Attachments = project.Attachments.Select(p => bAttachment.ConverEntityToModel(p)).ToList(),
                Ques = project.Ques.Select(p => bQue.ConverEntityToModel(p)).ToList()

            };
            return model;
        }


        private M_IntelInnovationProjectApplySearch ConverSearchEntityToModel(IntelInnovationProjectApplySearch project)
        {
            if (project == null) return null;


            var model = new M_IntelInnovationProjectApplySearch()
            {
                Id = project.Id.ToString(),
                Name = string.IsNullOrEmpty(project.Name) ? string.Empty : project.Name,
                Description = string.IsNullOrEmpty(project.Description) ? string.Empty : project.Description,
                OrganizationName = string.IsNullOrEmpty(project.OrganizationName) ? string.Empty : project.OrganizationName,
                FieldScopeCode = string.IsNullOrEmpty(project.FieldScopeCode) ? string.Empty : project.FieldScopeCode,
                FieldScopeName = string.IsNullOrEmpty(project.FieldScopeName) ? string.Empty : project.FieldScopeName,
                ProjectStage = string.IsNullOrEmpty(project.ProjectStage) ? string.Empty : project.ProjectStage,
                BusinessLicense = string.IsNullOrEmpty(project.BusinessLicense) ? string.Empty : project.BusinessLicense,
                FoundedTime = project.FoundedTime,
                BusinessAddress = string.IsNullOrEmpty(project.BusinessAddress) ? string.Empty : project.BusinessAddress,
                Website = string.IsNullOrEmpty(project.Website) ? string.Empty : project.Website,
                WebChatNumber = string.IsNullOrEmpty(project.WebChatNumber) ? string.Empty : project.WebChatNumber,
                Principal = string.IsNullOrEmpty(project.Principal) ? string.Empty : project.Principal,
                PrincipalPosition = string.IsNullOrEmpty(project.PrincipalPosition) ? string.Empty : project.PrincipalPosition,
                PrincipalPhone = string.IsNullOrEmpty(project.PrincipalPhone) ? string.Empty : project.PrincipalPhone,
                PrincipalMail = string.IsNullOrEmpty(project.PrincipalMail) ? string.Empty : project.PrincipalMail,
                Contact = string.IsNullOrEmpty(project.Contact) ? string.Empty : project.Contact,
                ContactPosition = string.IsNullOrEmpty(project.ContactPosition) ? string.Empty : project.ContactPosition,
                ContactPhone = string.IsNullOrEmpty(project.ContactPhone) ? string.Empty : project.ContactPhone,
                ContactMail = string.IsNullOrEmpty(project.ContactMail) ? string.Empty : project.ContactMail,
                OrganizationDescription = string.IsNullOrEmpty(project.OrganizationDescription) ? string.Empty : project.OrganizationDescription,
                CoreTechnology = string.IsNullOrEmpty(project.CoreTechnology) ? string.Empty : project.CoreTechnology,
                Keyword = string.IsNullOrEmpty(project.Keyword) ? string.Empty : project.Keyword,
                Auditor = string.IsNullOrEmpty(project.Auditor) ? string.Empty : project.Auditor,
                AuditDate = project.AuditDate,
                AuditRemark = project.AuditRemark,
                AuditStatus = project.AuditStatus,
                Application = project.Application,
                ApplicationDate = project.ApplicationDate

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
                    OrganizationName = model.OrganizationName,
                    FieldScopeCode = model.FieldScopeCode,
                    FieldScopeName = model.FieldScopeName,
                    ProjectStage = model.ProjectStage,
                    BusinessLicense = model.BusinessLicense,
                    FoundedTime = model.FoundedTime,
                    BusinessAddress = model.BusinessAddress,
                    Website = model.Website,
                    WebChatNumber = model.WebChatNumber,
                    Principal = model.Principal,
                    PrincipalPosition = model.PrincipalPosition,
                    PrincipalPhone = model.PrincipalPhone,
                    PrincipalMail = model.PrincipalMail,
                    Contact = model.Contact,
                    ContactPosition = model.ContactPosition,
                    ContactPhone = model.ContactPhone,
                    ContactMail = model.ContactMail,
                    OrganizationDescription = model.OrganizationDescription,
                    CoreTechnology = model.CoreTechnology,
                    Keyword = model.Keyword
                    
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
                    OrganizationName = model.OrganizationName,
                    FieldScopeCode = model.FieldScopeCode,
                    FieldScopeName = model.FieldScopeName,
                    ProjectStage = model.ProjectStage,
                    BusinessLicense =  model.BusinessLicense,
                    FoundedTime = model.FoundedTime,
                    BusinessAddress = model.BusinessAddress,
                    Website = model.Website,
                    WebChatNumber = model.WebChatNumber,
                    Principal = model.Principal,
                    PrincipalPosition = model.PrincipalPosition,
                    PrincipalPhone = model.PrincipalPhone,
                    PrincipalMail = model.PrincipalMail,
                    Contact = model.Contact,
                    ContactPosition = model.ContactPosition,
                    ContactPhone = model.ContactPhone,
                    ContactMail = model.ContactMail,
                    OrganizationDescription = model.OrganizationDescription,
                    CoreTechnology = model.CoreTechnology,
                    Keyword = model.Keyword

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

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="que"></param>
        /// <returns></returns>
        public Message Audit(string projectId,string AuditStatus,string AuditRemark)
        {
            Message message = new Message();
            //根据ProjectId找出最新的队列
            B_IntelInnovationProjectApplyQue bQue = new B_IntelInnovationProjectApplyQue();
            var ques = bQue.GetIntelInnovationProjectApplyQueByProjectId(projectId);
            var latestQue = ques.OrderByDescending(q => q.CreateDate).First();

            //构造新增数据
            M_IntelInnovationProjectApplyQue mQue = new M_IntelInnovationProjectApplyQue()
            {
                IntelInnovationProjectApplyId = projectId,
                Auditor = _currentUser.UserName,
                AuditDate = DateTime.Now,
                AuditRemark = AuditRemark,
                AuditStatus = AuditStatus,
                Application = latestQue.Application,
                ApplicationDate = latestQue.ApplicationDate,
                CreateDate = DateTime.Now,
                Creator = _currentUser.UserName
            };
            message = bQue.AddIntelInnovationProjectApplyQue(mQue);

            return message;
        }
    }
}
