using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_UserAttachment
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<UserAttachment> GetPageData(UserAttachment attachment, int skip, int limit)
        {
            var query = _dbContext.UserAttachments.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(attachment.UserId.ToString()))
                query = query.Where(a => a.UserId == attachment.UserId);

            #endregion

            var result = query.OrderBy(a => a.Id)
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        public int GetPageDataTotalCount(UserAttachment attachment)
        {
            var query = _dbContext.UserAttachments.Where(a => 1 == 1);

            var result = query.ToList().Count();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserAttachment GetUserAttachmentById(int id)
        {
            var attachment = _dbContext.UserAttachments.FirstOrDefault(a => a.Id == id);
            return attachment;
        }

        public List<UserAttachment> GetUserAttachmentByUserId(string userid)
        {
            var attachments = _dbContext.UserAttachments
                .Where(a => a.UserId == userid)
                .ToList();
            return attachments;
        }

        public List<UserAttachment> GetBuMulitCondition(UserAttachment attachment)
        {
            var query = _dbContext.UserAttachments.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(attachment.UserId))
                query = query.Where(a => a.UserId == attachment.UserId);
            if (!string.IsNullOrEmpty(attachment.Type))
                query = query.Where(a => a.Type == attachment.Type);
            #endregion

            var result = query.ToList();

            return result;
        }

        public List<UserAttachment> GetAll()
        {
            var contacts = _dbContext.UserAttachments
                .ToList();

            return contacts;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public int AddUserAttachment(UserAttachment attachment)
        {
            _dbContext.UserAttachments.Add(attachment);
            _dbContext.SaveChanges();
            return attachment.Id;
        }

        public int EditUserAttachment(int id, UserAttachment model)
        {
            var attachment = _dbContext.UserAttachments.FirstOrDefault(a => a.Id == id);

            attachment.VirtualPath = model.VirtualPath;
            attachment.PhysicalPath = model.PhysicalPath;
            attachment.Name = model.Name;
            attachment.SaveName = model.SaveName;
            attachment.Extension = model.Extension;

            _dbContext.SaveChanges();
            return attachment.Id;
        }



        public void DeleteUserAttachment(int id)
        {
            var attachment = _dbContext.UserAttachments.Where(i => i.Id == id).FirstOrDefault();
            if (attachment != null)
            {
                _dbContext.UserAttachments.Remove(attachment);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteUserAttachment(List<int> ids)
        {
            var contacts = _dbContext.UserAttachments.Where(i => ids.Contains(i.Id)).ToList();
            if (contacts.Count > 0)
            {
                _dbContext.UserAttachments.RemoveRange(contacts);
                _dbContext.SaveChanges();
            }
        }
    }
}
