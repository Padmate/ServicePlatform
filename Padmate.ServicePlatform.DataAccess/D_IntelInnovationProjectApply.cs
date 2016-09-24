﻿using Padmate.ServicePlatform.Entities;
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
            var query = _dbContext.Database.SqlQuery<IntelInnovationProjectApplySearch>(sql);

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


            var query = _dbContext.Database.SqlQuery<IntelInnovationProjectApplySearch>(sql);

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
            projcet.HasExample = model.HasExample;
            projcet.InnovationPoint = model.InnovationPoint;
            projcet.ContactPhone = model.ContactPhone;
            projcet.Contact = model.Contact;

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
