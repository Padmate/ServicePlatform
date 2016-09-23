using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_IntelInnovationProjectApplyAttachment
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<IntelInnovationProjectApplyAttachment> GetPageData(IntelInnovationProjectApplyAttachment attachment, int skip, int limit)
        {
            var query = _dbContext.IntelInnovationProjectApplyAttachments.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(attachment.IntelInnovationProjectApplyId.ToString()))
                query = query.Where(a => a.IntelInnovationProjectApplyId == attachment.IntelInnovationProjectApplyId);

            #endregion

            var result = query.OrderBy(a => a.Id)
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        public int GetPageDataTotalCount(IntelInnovationProjectApplyAttachment attachment)
        {
            var query = _dbContext.IntelInnovationProjectApplyAttachments.Where(a => 1 == 1);

            var result = query.ToList().Count();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IntelInnovationProjectApplyAttachment GetIntelInnovationProjectApplyAttachmentById(int id)
        {
            var attachment = _dbContext.IntelInnovationProjectApplyAttachments.FirstOrDefault(a => a.Id == id);
            return attachment;
        }

        public List<IntelInnovationProjectApplyAttachment> GetIntelInnovationProjectApplyAttachmentByProjectId(int id)
        {
            var attachments = _dbContext.IntelInnovationProjectApplyAttachments
                .Where(a => a.IntelInnovationProjectApplyId == id)
                .ToList();
            return attachments;
        }


        public List<IntelInnovationProjectApplyAttachment> GetAll()
        {
            var contacts = _dbContext.IntelInnovationProjectApplyAttachments
                .ToList();

            return contacts;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public int AddIntelInnovationProjectApplyAttachment(IntelInnovationProjectApplyAttachment attachment)
        {
            _dbContext.IntelInnovationProjectApplyAttachments.Add(attachment);
            _dbContext.SaveChanges();
            return attachment.Id;
        }

        public int EditIntelInnovationProjectApplyAttachment(int id, IntelInnovationProjectApplyAttachment model)
        {
            var attachment = _dbContext.IntelInnovationProjectApplyAttachments.FirstOrDefault(a => a.Id == id);

            attachment.VirtualPath = model.VirtualPath;
            attachment.PhysicalPath = model.PhysicalPath;
            attachment.Name = model.Name;
            attachment.SaveName = model.SaveName;
            attachment.Extension = model.Extension;

            _dbContext.SaveChanges();
            return attachment.Id;
        }



        public void DeleteIntelInnovationProjectApplyAttachment(int id)
        {
            var attachment = _dbContext.IntelInnovationProjectApplyAttachments.Where(i => i.Id == id).FirstOrDefault();
            if (attachment != null)
            {
                _dbContext.IntelInnovationProjectApplyAttachments.Remove(attachment);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteIntelInnovationProjectApplyAttachment(List<int> ids)
        {
            var contacts = _dbContext.IntelInnovationProjectApplyAttachments.Where(i => ids.Contains(i.Id)).ToList();
            if (contacts.Count > 0)
            {
                _dbContext.IntelInnovationProjectApplyAttachments.RemoveRange(contacts);
                _dbContext.SaveChanges();
            }
        }
    }
}
