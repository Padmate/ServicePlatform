using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_ContactScope
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="contactScope"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<ContactScope> GetPageData(ContactScope contactScope, int skip, int limit)
        {
            var query = _dbContext.ContactScopes.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(contactScope.Scope))
                query = query.Where(a => a.Scope.Contains(contactScope.Scope));

            #endregion

            var result = query.OrderBy(a => new { a.Sequence })
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        public List<ContactScope> GetAll()
        {
            var result = _dbContext.ContactScopes.OrderBy(a => a.Sequence).ToList() ;
            return result;
        }

        public int GetPageDataTotalCount(ContactScope contactScope)
        {
            var query = _dbContext.ContactScopes.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(contactScope.Scope))
                query = query.Where(a => a.Scope.Contains(contactScope.Scope));

            #endregion

            var result = query.ToList().Count();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactScope GetContactScopeById(int id)
        {
            var contactScope = _dbContext.ContactScopes.FirstOrDefault(a => a.Id == id);
            return contactScope;
        }

        public ContactScope GetContactScopeByScope(string scope)
        {
            var contactScope = _dbContext.ContactScopes.FirstOrDefault(a => a.Scope == scope);
            return contactScope;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="contactScope"></param>
        /// <returns></returns>
        public int AddContactScope(ContactScope contactScope)
        {
            _dbContext.ContactScopes.Add(contactScope);
            _dbContext.SaveChanges();
            return contactScope.Id;
        }

        public int EditContactScope(int id, ContactScope model)
        {
            var contactScope = _dbContext.ContactScopes.FirstOrDefault(a => a.Id == id);

            contactScope.Scope = model.Scope;
            contactScope.Sequence = model.Sequence;

            _dbContext.SaveChanges();
            return contactScope.Id;
        }


        public void DeleteContactScope(int id)
        {
            var contactScope = _dbContext.ContactScopes.Where(i => i.Id == id).FirstOrDefault();
            if (contactScope != null)
            {
                _dbContext.ContactScopes.Remove(contactScope);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteContactScope(List<int> ids)
        {
            var contactScopes = _dbContext.ContactScopes.Where(i => ids.Contains(i.Id)).ToList();
            if (contactScopes.Count > 0)
            {
                _dbContext.ContactScopes.RemoveRange(contactScopes);
                _dbContext.SaveChanges();
            }
        }
    }
}
