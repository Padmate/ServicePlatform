using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_Project
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="projcet"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Project> GetPageData(Project projcet, int skip, int limit)
        {
            var query = _dbContext.Projects.Include("ProjectDownloads").Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(projcet.Name))
                query = query.Where(a => a.Name.Contains(projcet.Name));

            #endregion

            var result = query.OrderBy(a =>a.Sequence )
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        public int GetPageDataTotalCount(Project projcet)
        {
            var query = _dbContext.Projects.Where(a => 1 == 1);

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
        public Project GetProjectById(string id)
        {
            var projcet = _dbContext.Projects.Include("ProjectDownloads").FirstOrDefault(a => a.Id.ToString() == id);
            return projcet;
        }


        public List<Project> GetAll()
        {
            var contacts = _dbContext.Projects.Include("ProjectDownloads")
                .ToList();

            return contacts;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="projcet"></param>
        /// <returns></returns>
        public string AddProject(Project projcet)
        {
            _dbContext.Projects.Add(projcet);
            _dbContext.SaveChanges();
            return projcet.Id.ToString();
        }

        public string EditProject(string id, Project model)
        {
            var projcet = _dbContext.Projects.FirstOrDefault(a => a.Id.ToString() == id);

            projcet.Name = model.Name;
            projcet.Description = model.Description;
            projcet.Content = model.Content;
            projcet.Type = model.Type;
            projcet.Sequence = model.Sequence;
            projcet.Sequence = model.Sequence;
            projcet.Modifier = model.Modifier;
            projcet.ModifiedDate = model.ModifiedDate;

            _dbContext.SaveChanges();
            return projcet.Id.ToString();
        }

        public string EditImageId(string id, int imageId)
        {
            var projcet = _dbContext.Projects.FirstOrDefault(a => a.Id.ToString() == id);

            projcet.ImageId = imageId;

            _dbContext.SaveChanges();
            return projcet.Id.ToString();
        }

        public void DeleteProject(string id)
        {
            var projcet = _dbContext.Projects.Where(i => i.Id.ToString() == id).FirstOrDefault();
            if (projcet != null)
            {
                _dbContext.Projects.Remove(projcet);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteProject(List<string> ids)
        {
            var contacts = _dbContext.Projects.Where(i => ids.Contains(i.Id.ToString())).ToList();
            if (contacts.Count > 0)
            {
                _dbContext.Projects.RemoveRange(contacts);
                _dbContext.SaveChanges();
            }
        }
    }
}
