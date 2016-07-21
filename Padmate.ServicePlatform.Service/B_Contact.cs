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
    public class B_Contact
    {
        D_Contact _dContact = new D_Contact();

        public M_Contact GetById(int id)
        {
            var contact = _dContact.GetContactById(id);
            var result = ConverEntityToModel(contact);
            return result;
        }

        public List<M_Contact> GetAllData()
        {
            var contacts = _dContact.GetAll();
            var result = contacts.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public List<M_Contact> GetPageData(M_Contact contact)
        {
            Contact searchModel = new Contact()
            {
                Name = contact.Name
            };

            var offset = contact.offset;
            var limit = contact.limit;


            var pageResult = _dContact.GetPageData(searchModel, offset, limit);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(M_Contact contact)
        {
            Contact searchModel = new Contact()
            {
                Name = contact.Name
            };

            var totalCount = _dContact.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        private M_Contact ConverEntityToModel(Contact contact)
        {
            if (contact == null) return null;

            B_ContactScope bContactScope = new B_ContactScope();
            var model = new M_Contact()
            {
                Id = contact.Id.ToString(),
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Description = contact.Description,
                Email = contact.Email,
                Sequence =contact.Sequence.ToString(),
                ContactScopeId = contact.ContactScopeId.ToString(),
                Scope = contact.ContactScope.Scope
            };
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddContact(M_Contact model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "联系人新增成功";

            try
            {
                //新增联系人
                var contact = new Contact()
                {
                    ContactScopeId = System.Convert.ToInt32(model.ContactScopeId),
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    Sequence = string.IsNullOrEmpty(model.Sequence) ? 0 : System.Convert.ToInt32(model.Sequence),
                };

                message.ReturnId = _dContact.AddContact(contact);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "联系人新增失败，异常：" + e.Message;
            }
            return message;
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Message EditContact(M_Contact model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "联系人修改成功";

            try
            {
                var contact = new Contact()
                {
                    ContactScopeId = System.Convert.ToInt32(model.ContactScopeId),
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    Sequence = string.IsNullOrEmpty(model.Sequence) ? 0 : System.Convert.ToInt32(model.Sequence),
                };

                message.ReturnId = _dContact.EditContact(System.Convert.ToInt32(model.Id), contact);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "联系人修改失败，异常：" + e.Message;
            }
            return message;
        }

        public Message BatchDeleteByIds(List<int> ids)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "联系人删除成功";

            try
            {
                _dContact.BatchDeleteContact(ids);

            }catch(Exception e)
            {
                message.Success = false;
                message.Content = "联系人删除失败："+e.Message;
            }

            return message;
        }

    }
}
