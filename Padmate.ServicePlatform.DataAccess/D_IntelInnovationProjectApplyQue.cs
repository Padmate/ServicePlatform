using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_IntelInnovationProjectApplyQue
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="que"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<IntelInnovationProjectApplyQue> GetPageData(IntelInnovationProjectApplyQue que, int skip, int limit)
        {
            var query = _dbContext.IntelInnovationProjectApplyQues.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(que.IntelInnovationProjectApplyId.ToString()))
                query = query.Where(a => a.IntelInnovationProjectApplyId == que.IntelInnovationProjectApplyId);

            #endregion

            var result = query.OrderBy(a => a.Id)
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        public int GetPageDataTotalCount(IntelInnovationProjectApplyQue que)
        {
            var query = _dbContext.IntelInnovationProjectApplyQues.Where(a => 1 == 1);

            var result = query.ToList().Count();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IntelInnovationProjectApplyQue GetIntelInnovationProjectApplyQueById(int id)
        {
            var que = _dbContext.IntelInnovationProjectApplyQues.FirstOrDefault(a => a.Id == id);
            return que;
        }


        /// <summary>
        /// 根据IntelInnovationProjectApplyID查找
        /// </summary>
        /// <param name="intelInnovationProjectApplyId"></param>
        /// <returns></returns>
        public List<IntelInnovationProjectApplyQue> GetIntelInnovationProjectApplyQueByProjectId(int intelInnovationProjectApplyId)
        {
            var attachments = _dbContext.IntelInnovationProjectApplyQues
                .Where(a => a.IntelInnovationProjectApplyId == intelInnovationProjectApplyId)
                .ToList();
            return attachments;
        }


        public List<IntelInnovationProjectApplyQue> GetAll()
        {
            var contacts = _dbContext.IntelInnovationProjectApplyQues
                .ToList();

            return contacts;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="que"></param>
        /// <returns></returns>
        public int AddIntelInnovationProjectApplyQue(IntelInnovationProjectApplyQue que)
        {
            _dbContext.IntelInnovationProjectApplyQues.Add(que);
            _dbContext.SaveChanges();
            return que.Id;
        }

        public int EditIntelInnovationProjectApplyQue(int id, IntelInnovationProjectApplyQue model)
        {
            var que = _dbContext.IntelInnovationProjectApplyQues.FirstOrDefault(a => a.Id == id);

            que.Auditor = model.Auditor;
            que.AuditDate = model.AuditDate;
            que.AuditStatus = model.AuditStatus;
            que.AuditRemark = model.AuditRemark;


            _dbContext.SaveChanges();
            return que.Id;
        }



        public void DeleteIntelInnovationProjectApplyQue(int id)
        {
            var que = _dbContext.IntelInnovationProjectApplyQues.Where(i => i.Id == id).FirstOrDefault();
            if (que != null)
            {
                _dbContext.IntelInnovationProjectApplyQues.Remove(que);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteIntelInnovationProjectApplyQue(List<int> ids)
        {
            var contacts = _dbContext.IntelInnovationProjectApplyQues.Where(i => ids.Contains(i.Id)).ToList();
            if (contacts.Count > 0)
            {
                _dbContext.IntelInnovationProjectApplyQues.RemoveRange(contacts);
                _dbContext.SaveChanges();
            }
        }
    }
}
