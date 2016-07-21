using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_Contact
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Contact> GetPageData(Contact contact, int skip, int limit)
        {
            var query = _dbContext.Contacts.Include("ContactScope").Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(contact.Name))
                query = query.Where(a => a.Name.Contains(contact.Name));

            #endregion

            var result = query.OrderBy(a => new { a.ContactScopeId,a.Sequence })
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        public int GetPageDataTotalCount(Contact contact)
        {
            var query = _dbContext.Contacts.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(contact.Name))
                query = query.Where(a => a.Name.Contains(contact.Name));

            #endregion

            var result = query.ToList().Count();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact GetContactById(int id)
        {
            var contact = _dbContext.Contacts.Include("ContactScope").FirstOrDefault(a => a.Id == id);
            return contact;
        }


        public List<Contact> GetAll()
        {
            var contacts = _dbContext.Contacts.Include("ContactScope")
                .ToList();

            return contacts;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public int AddContact(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
            return contact.Id;
        }

        public int EditContact(int id, Contact model)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(a => a.Id == id);

            contact.ContactScopeId = model.ContactScopeId;
            contact.Name = model.Name;
            contact.Email = model.Email;
            contact.PhoneNumber = model.PhoneNumber;
            contact.Description = model.Description;
            contact.Sequence = model.Sequence;

            _dbContext.SaveChanges();
            return contact.Id;
        }


        public void DeleteContact(int id)
        {
            var contact = _dbContext.Contacts.Where(i => i.Id == id).FirstOrDefault();
            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteContact(List<int> ids)
        {
            var contacts = _dbContext.Contacts.Where(i => ids.Contains(i.Id)).ToList();
            if (contacts.Count >0)
            {
                _dbContext.Contacts.RemoveRange(contacts);
                _dbContext.SaveChanges();
            }
        }
    }
}
