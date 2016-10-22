using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_Module
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        
        /// <summary>
        /// 根据类型获取模块，返回前limit条数据
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<Module> GetModulesByType(string moduleType, int? limit)
        {
            var queryData = _dbContext.Modules
                .Where(a => a.Type == moduleType)
                .OrderBy(a => a.Sequence);

            if (limit != null)
            {
                var modules = queryData.Take(System.Convert.ToInt32(limit))
                    .ToList();
                return modules;
            }

            return queryData.ToList(); ;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="module"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Module> GetPageData(Module module, int skip, int limit)
        {
            var query = _dbContext.Modules.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(module.Type))
                query = query.Where(a => a.Type == module.Type);

            if (!string.IsNullOrEmpty(module.SubTitle))
                query = query.Where(a => a.SubTitle.Contains(module.SubTitle));

            
            #endregion

            var result = query.OrderBy(a => a.Sequence)
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        public int GetPageDataTotalCount(Module module)
        {
            var query = _dbContext.Modules.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(module.Type))
                query = query.Where(a => a.Type == module.Type);

            if (!string.IsNullOrEmpty(module.SubTitle))
                query = query.Where(a => a.SubTitle.Contains(module.SubTitle));
            #endregion

            var result = query.ToList().Count();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Module GetModuleById(int id)
        {
            var module = _dbContext.Modules.FirstOrDefault(a => a.Id == id);
            return module;
        }

        public Module GetModuleById(string urlId)
        {
            var module = _dbContext.Modules.FirstOrDefault(a => a.ModuleURLId == urlId);
            return module;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public int AddModule(Module module)
        {
            _dbContext.Modules.Add(module);
            _dbContext.SaveChanges();
            return module.Id;
        }

        public int EditModule(int id, Module model)
        {
            var module = _dbContext.Modules.FirstOrDefault(a => a.Id == id);

            module.Title = model.Title;
            module.SubTitle = model.SubTitle;
            module.Description = model.Description;
            module.Content = model.Content;
            module.ModifiedDate = model.ModifiedDate;
            module.Modifier = model.Modifier;
            module.Sequence = model.Sequence;
            module.IsHref = model.IsHref;
            module.Href = model.Href;
            module.ModuleURLId = model.ModuleURLId;
            

            _dbContext.SaveChanges();
            return module.Id;
        }

        public int EditImageId(int id, int imageId)
        {
            var module = _dbContext.Modules.FirstOrDefault(a => a.Id == id);

            module.ImageId = imageId;

            _dbContext.SaveChanges();
            return module.Id;
        }

        public void DeleteModule(int id)
        {
            var module = _dbContext.Modules.Where(i => i.Id == id).FirstOrDefault();
            if (module != null)
            {
                _dbContext.Modules.Remove(module);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDelete(List<int> ids)
        {
            var modules = _dbContext.Modules.Where(i => ids.Contains(i.Id)).ToList();
            if (modules.Count > 0)
            {
                _dbContext.Modules.RemoveRange(modules);
                _dbContext.SaveChanges();
            }
        }
    }
}
