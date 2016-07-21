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
    public class B_ContactScope
    {
        D_ContactScope _dContactScope = new D_ContactScope();

        public M_ContactScope GetById(int id)
        {
            var contactScope = _dContactScope.GetContactScopeById(id);
            var result = ConverEntityToModel(contactScope);
            return result;
        }

        public M_ContactScope GetByScope(string scope)
        {
            var contactScope = _dContactScope.GetContactScopeByScope(scope);
            var result = ConverEntityToModel(contactScope);
            return result;
        }

        public List<M_ContactScope> GetAllData()
        {
            var contactScopes = _dContactScope.GetAll();
            var result = contactScopes.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="contactScope"></param>
        /// <returns></returns>
        public List<M_ContactScope> GetPageData(M_ContactScope contactScope)
        {
            ContactScope searchModel = new ContactScope()
            {
                Scope = contactScope.Scope
            };

            var offset = contactScope.offset;
            var limit = contactScope.limit;


            var pageResult = _dContactScope.GetPageData(searchModel, offset, limit);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(M_ContactScope contactScope)
        {
            ContactScope searchModel = new ContactScope()
            {
                Scope = contactScope.Scope
            };

            var totalCount = _dContactScope.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        private M_ContactScope ConverEntityToModel(ContactScope contactScope)
        {
            if (contactScope == null) return null;

            var model = new M_ContactScope()
            {
                Id = contactScope.Id.ToString(),
                Scope = contactScope.Scope,
                Sequence = contactScope.Sequence.ToString(),
            };
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddContactScope(M_ContactScope model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "联系人职责范围新增成功";

            try
            {
                //新增联系人
                var contactScope = new ContactScope()
                {
                    Scope = model.Scope,
                    Sequence = string.IsNullOrEmpty(model.Sequence) ? 0 : System.Convert.ToInt32(model.Sequence),
                };

                message.ReturnId = _dContactScope.AddContactScope(contactScope);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "联系人职责范围新增失败，异常：" + e.Message;
            }
            return message;
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Message EditContactScope(M_ContactScope model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "联系人职责范围修改成功";

            try
            {
                var contactScope = new ContactScope()
                {
                    Scope = model.Scope,
                    Sequence = string.IsNullOrEmpty(model.Sequence) ? 0 : System.Convert.ToInt32(model.Sequence),
                };

                message.ReturnId = _dContactScope.EditContactScope(System.Convert.ToInt32(model.Id), contactScope);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "联系人职责范围修改失败，异常：" + e.Message;
            }
            return message;
        }

        public Message BatchDeleteByIds(List<int> ids)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "联系人职责范围删除成功";

            try
            {
                _dContactScope.BatchDeleteContactScope(ids);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "联系人职责范围删除失败：" + e.Message;
            }

            return message;
        }
    }
}
