using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_IntelInnovationProjectApply
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="projcet"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<IntelInnovationProjectApply> GetPageData(IntelInnovationProjectApply projcet, int skip, int limit)
        {
            var query = _dbContext.IntelInnovationProjectApplies
                .Include("Attachments").Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(projcet.Name))
                query = query.Where(a => a.Name.Contains(projcet.Name));

            #endregion

            var result = query.OrderBy(a => a.Id)
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        /// <summary>
        /// 获取审核分页数据
        /// </summary>
        /// <returns></returns>
        public List<IntelInnovationProjectApplySearch> GetPageDataForAudit(IntelInnovationProjectApplySearch projcet, int skip, int limit)
        {
            var sql = @"select app.*,
                        que.Id as QueId,
                        que.Auditor,
                        que.AuditDate,
                        que.AuditRemark,
                        que.AuditStatus,
                        que.Application,
                        que.ApplicationDate
                        from 
                        --查找最新的队列
                        (select IntelInnovationProjectApplyId,max(CreateDate) CreateDate
                        from dbo.IntelInnovationProjectApplyQues group by IntelInnovationProjectApplyId) latestQue
                        --左链接基础数据
                        left join dbo.IntelInnovationProjectApplies app
                        on app.Id = latestQue.IntelInnovationProjectApplyId
                        --左链接que
                        left join dbo.IntelInnovationProjectApplyQues que
                        on que.CreateDate = latestQue.CreateDate";

            //var args = new DbParameter[] {
            //      new SqlParameter {ParameterName = "culture", Value = culture}};
            //var locations = _dbContext.Database.SqlQuery<string>(sql, args);
            var querySql = _dbContext.Database.SqlQuery<IntelInnovationProjectApplySearch>(sql);

            var query = querySql.Where(a=> 1==1);
            #region　条件过滤
            if (!string.IsNullOrEmpty(projcet.Name))
                query = query.Where(a => a.Name.Contains(projcet.Name));

            if (!string.IsNullOrEmpty(projcet.AuditStatus))
                query = query.Where(a => a.AuditStatus.Contains(projcet.AuditStatus));

            #endregion

            var result = query.OrderByDescending(a => a.ApplicationDate)
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result.ToList();
        }

        public int GetPageDataTotalCountForAudit(IntelInnovationProjectApplySearch projcet)
        {
            var sql = @"select app.*,
                        que.Id as QueId,
                        que.Auditor,
                        que.AuditDate,
                        que.AuditRemark,
                        que.AuditStatus,
                        que.Application,
                        que.ApplicationDate
                        from 
                        --查找最新的队列
                        (select IntelInnovationProjectApplyId,max(CreateDate) CreateDate
                        from dbo.IntelInnovationProjectApplyQues group by IntelInnovationProjectApplyId) latestQue
                        --左链接基础数据
                        left join dbo.IntelInnovationProjectApplies app
                        on app.Id = latestQue.IntelInnovationProjectApplyId
                        --左链接que
                        left join dbo.IntelInnovationProjectApplyQues que
                        on que.CreateDate = latestQue.CreateDate";



            var querySql = _dbContext.Database.SqlQuery<IntelInnovationProjectApplySearch>(sql);

            var query = querySql.Where(qs => 1 == 1);
            #region　条件过滤
            if (!string.IsNullOrEmpty(projcet.Name))
                query = query.Where(a => a.Name.Contains(projcet.Name));

            if (!string.IsNullOrEmpty(projcet.AuditStatus))
                query = query.Where(a => a.AuditStatus.Contains(projcet.AuditStatus));

            #endregion

            var result = query.Count();

            return result;
        }


        public int GetPageDataTotalCount(IntelInnovationProjectApply projcet)
        {
            var query = _dbContext.IntelInnovationProjectApplies.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(projcet.Name))
                query = query.Where(a => a.Name.Contains(projcet.Name));

            #endregion

            var result = query.ToList().Count();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IntelInnovationProjectApply GetIntelInnovationProjectApplyById(int id)
        {
            var projcet = _dbContext.IntelInnovationProjectApplies
                .Include("Attachments")
                .FirstOrDefault(a => a.Id == id);
            return projcet;
        }

        /// <summary>
        /// 根据用户ID查找
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<IntelInnovationProjectApply> GetIntelInnovationProjectApplyByUserId(string UserId)
        {
            var projcets = _dbContext.IntelInnovationProjectApplies
                .Include("Attachments")
                .Include("Ques")
                .Where(a => a.UserId == UserId)
                .ToList();

            return projcets;
        }

        public List<IntelInnovationProjectApply> GetAll()
        {
            var contacts = _dbContext.IntelInnovationProjectApplies.Include("Attachments")
                .ToList();

            return contacts;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="projcet"></param>
        /// <returns></returns>
        public string AddIntelInnovationProjectApply(IntelInnovationProjectApply projcet)
        {
            _dbContext.IntelInnovationProjectApplies.Add(projcet);
            _dbContext.SaveChanges();
            return projcet.Id.ToString();
        }

        public int EditIntelInnovationProjectApply(int id, IntelInnovationProjectApply model)
        {
            var projcet = _dbContext.IntelInnovationProjectApplies.FirstOrDefault(a => a.Id == id);

            projcet.Name = model.Name;
            projcet.Description = model.Description;
            projcet.OrganizationName = model.OrganizationName;
            projcet.FieldScopeCode = model.FieldScopeCode;
            projcet.FieldScopeName = model.FieldScopeName;
            projcet.ProjectStage = model.ProjectStage;
            projcet.BusinessLicense = model.BusinessLicense;
            projcet.FoundedTime = model.FoundedTime;
            projcet.BusinessAddress = model.BusinessAddress;
            projcet.Website = model.Website;
            projcet.WebChatNumber = model.WebChatNumber;
            projcet.Principal = model.Principal;
            projcet.PrincipalPosition = model.PrincipalPosition;
            projcet.PrincipalPhone = model.PrincipalPhone;
            projcet.PrincipalMail = model.PrincipalMail;
            projcet.Contact = model.Contact;
            projcet.ContactPosition = model.ContactPosition;
            projcet.ContactPhone = model.ContactPhone;
            projcet.ContactMail = model.ContactMail;
            projcet.OrganizationDescription = model.OrganizationDescription;
            projcet.CoreTechnology = model.CoreTechnology;
            projcet.Keyword = model.Keyword;


            _dbContext.SaveChanges();
            return projcet.Id;
        }



        public void DeleteIntelInnovationProjectApply(string id)
        {
            var projcet = _dbContext.IntelInnovationProjectApplies.Where(i => i.Id.ToString() == id).FirstOrDefault();
            if (projcet != null)
            {
                _dbContext.IntelInnovationProjectApplies.Remove(projcet);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteIntelInnovationProjectApply(List<string> ids)
        {
            var contacts = _dbContext.IntelInnovationProjectApplies.Where(i => ids.Contains(i.Id.ToString())).ToList();
            if (contacts.Count > 0)
            {
                _dbContext.IntelInnovationProjectApplies.RemoveRange(contacts);
                _dbContext.SaveChanges();
            }
        }
    }
}
