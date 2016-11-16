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
                        que.ApplicationDate,
                        u.UserType
                        from 
                        --查找最新的队列
                        (select IntelInnovationProjectApplyId,max(CreateDate) CreateDate
                        from dbo.IntelInnovationProjectApplyQues group by IntelInnovationProjectApplyId) latestQue
                        --左链接基础数据
                        left join dbo.IntelInnovationProjectApplies app
                        on app.Id = latestQue.IntelInnovationProjectApplyId
                        --左链接que
                        left join dbo.IntelInnovationProjectApplyQues que
                        on que.CreateDate = latestQue.CreateDate
                        --左链接用户信息
						left join dbo.AspNetUsers u
						on app.UserId = u.Id";

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

            if (!string.IsNullOrEmpty(projcet.UserType))
                query = query.Where(a => a.UserType == projcet.UserType);
            if (!string.IsNullOrEmpty(projcet.OrganizationName))
                query = query.Where(a => a.OrganizationName.Contains(projcet.OrganizationName));
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
                        que.ApplicationDate,
                        u.UserType
                        from 
                        --查找最新的队列
                        (select IntelInnovationProjectApplyId,max(CreateDate) CreateDate
                        from dbo.IntelInnovationProjectApplyQues group by IntelInnovationProjectApplyId) latestQue
                        --左链接基础数据
                        left join dbo.IntelInnovationProjectApplies app
                        on app.Id = latestQue.IntelInnovationProjectApplyId
                        --左链接que
                        left join dbo.IntelInnovationProjectApplyQues que
                        on que.CreateDate = latestQue.CreateDate
                        --左链接用户信息
						left join dbo.AspNetUsers u
						on app.UserId = u.Id";



            var querySql = _dbContext.Database.SqlQuery<IntelInnovationProjectApplySearch>(sql);

            var query = querySql.Where(qs => 1 == 1);
            #region　条件过滤
            if (!string.IsNullOrEmpty(projcet.Name))
                query = query.Where(a => a.Name.Contains(projcet.Name));

            if (!string.IsNullOrEmpty(projcet.AuditStatus))
                query = query.Where(a => a.AuditStatus.Contains(projcet.AuditStatus));

            if (!string.IsNullOrEmpty(projcet.UserType))
                query = query.Where(a => a.UserType == projcet.UserType);
            if (!string.IsNullOrEmpty(projcet.OrganizationName))
                query = query.Where(a => a.OrganizationName.Contains(projcet.OrganizationName));
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


        public List<IntelInnovationProjectApplySearch> GetPageDataForVote(IntelInnovationProjectApplySearch projcet, int skip, int limit)
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
                        on que.CreateDate = latestQue.CreateDate
						--过滤确认切投票编号不为空的数据
						where VoteNo is not null and AuditStatus ='1'";

            var querySql = _dbContext.Database.SqlQuery<IntelInnovationProjectApplySearch>(sql);

            var query = querySql.Where(a => 1 == 1);
            #region　条件过滤
            if (!string.IsNullOrEmpty(projcet.Name))
                query = query.Where(a => a.Name.Contains(projcet.Name));

            if (projcet.VoteNo != null)
                query = query.Where(a => a.VoteNo == projcet.VoteNo);
            if(!string.IsNullOrEmpty(projcet.Search))
            {
                query = query.Where(a => a.VoteNo.ToLower().Contains(projcet.Search.ToLower()) 
                    || a.Name.ToLower().Contains(projcet.Search.ToLower()));

            }
            #endregion

            var votePrifix = VoteNoPrefix();
            var result = query.OrderBy(a => System.Convert.ToInt32(a.VoteNo))
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result.ToList();
        }

        public int GetPageDataTotalCountForVote(IntelInnovationProjectApplySearch projcet)
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
                        on que.CreateDate = latestQue.CreateDate
						--过滤确认切投票编号不为空的数据
						where VoteNo is not null and AuditStatus ='1'";



            var querySql = _dbContext.Database.SqlQuery<IntelInnovationProjectApplySearch>(sql);

            var query = querySql.Where(qs => 1 == 1);
            #region　条件过滤
            if (!string.IsNullOrEmpty(projcet.Name))
                query = query.Where(a => a.Name.Contains(projcet.Name));

            if (projcet.VoteNo != null)
                query = query.Where(a => a.VoteNo == projcet.VoteNo);

            if (!string.IsNullOrEmpty(projcet.Search))
            {
                query = query.Where(a => a.VoteNo.ToLower().Contains(projcet.Search.ToLower())
                    || a.Name.ToLower().Contains(projcet.Search.ToLower()));
            }
            #endregion

            var result = query.Count();

            return result;
        }

        public List<IntelInnovationProjectApplySearch> GetVoteResults()
        {
            var sql = @"select a.* from IntelInnovationProjectApplies a where VoteNo is not null";

            var query = _dbContext.Database.SqlQuery<IntelInnovationProjectApplySearch>(sql);

            var votePrifix = VoteNoPrefix();
            var result = query.OrderByDescending(a=>a.TotalVotes)
            .ThenBy(a => System.Convert.ToInt32(a.VoteNo))
            .ToList();

            return result.ToList();
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
        /// 根据VoteNo查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IntelInnovationProjectApply GetIntelInnovationProjectApplyVoteNo(string voteno)
        {
            var projcet = _dbContext.IntelInnovationProjectApplies
                .FirstOrDefault(a => a.VoteNo == voteno);
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
            projcet.VoteNo = model.VoteNo;
            projcet.TotalVotes = model.TotalVotes;


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

        /// <summary>
        /// 投票编号前缀
        /// </summary>
        /// <returns></returns>
        public string VoteNoPrefix()
        {
            return "";
        }
        
        /// <summary>
        /// 获取最大的编号数
        /// </summary>
        /// <returns></returns>
        public int GetMaxVoteNo()
        {
            //编号前缀
            var votePrefix = VoteNoPrefix();

            var returnVoteNo = 0;
            var sql = @"select max(votenum) as maxvoteno from
                        (
                            select convert(int,replace(a.voteno,'{0}','')) as votenum from IntelInnovationProjectApplies a

                        )temp";
            sql = string.Format(sql,votePrefix);
            var maxVoteNo = _dbContext.Database.SqlQuery<int?>(sql).FirstOrDefault();
            if (maxVoteNo == null)
            {
                
                returnVoteNo = 0;
            }
            else
            {
                returnVoteNo = System.Convert.ToInt32(maxVoteNo);
            }

                
            return returnVoteNo;
        }

        /// <summary>
        /// 修改投票编号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="voteNo"></param>
        /// <returns></returns>
        public int EditVoteNo(int id,string voteNo)
        {
            var projcet = _dbContext.IntelInnovationProjectApplies.FirstOrDefault(a => a.Id == id);

            projcet.VoteNo = voteNo;
            _dbContext.SaveChanges();
            return projcet.Id;
        }

        public int EditTotalVotes(int id, int totalVotes)
        {
            var projcet = _dbContext.IntelInnovationProjectApplies.FirstOrDefault(a => a.Id == id);

            projcet.TotalVotes = totalVotes;
            _dbContext.SaveChanges();
            return projcet.Id;
        }

       
    }
}
