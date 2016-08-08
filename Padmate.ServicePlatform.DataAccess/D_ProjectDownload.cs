using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_ProjectDownload
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="projectDownload"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<ProjectDownload> GetPageData(ProjectDownload projectDownload, int skip, int limit)
        {
            var query = _dbContext.ProjectDownloads.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(projectDownload.ProjectId.ToString()))
                query = query.Where(a => a.ProjectId == projectDownload.ProjectId);

            #endregion

            var result = query.OrderBy(a => a.Sequence)
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        public int GetPageDataTotalCount(ProjectDownload projectDownload)
        {
            var query = _dbContext.ProjectDownloads.Where(a => 1 == 1);

            var result = query.ToList().Count();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectDownload GetProjectDownloadById(string id)
        {
            var projectDownload = _dbContext.ProjectDownloads.FirstOrDefault(a => a.Id.ToString() == id);
            return projectDownload;
        }


        public List<ProjectDownload> GetAll()
        {
            var contacts = _dbContext.ProjectDownloads
                .ToList();

            return contacts;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="projectDownload"></param>
        /// <returns></returns>
        public string AddProjectDownload(ProjectDownload projectDownload)
        {
            _dbContext.ProjectDownloads.Add(projectDownload);
            _dbContext.SaveChanges();
            return projectDownload.Id.ToString();
        }

        public string EditProjectDownload(string id, ProjectDownload model)
        {
            var projectDownload = _dbContext.ProjectDownloads.FirstOrDefault(a => a.Id.ToString() == id);

            projectDownload.Description = model.Description;
            projectDownload.VirtualPath = model.VirtualPath;
            projectDownload.PhysicalPath = model.PhysicalPath;
            projectDownload.Sequence = model.Sequence;
            projectDownload.SaveName = model.SaveName;
            projectDownload.Extension = model.Extension;

            _dbContext.SaveChanges();
            return projectDownload.Id.ToString();
        }

        public string EditImageId(string id, int imageId)
        {
            var projectDownload = _dbContext.ProjectDownloads.FirstOrDefault(a => a.Id.ToString() == id);

            projectDownload.ImageId = imageId;

            _dbContext.SaveChanges();
            return projectDownload.Id.ToString();
        }

        public void DeleteProjectDownload(string id)
        {
            var projectDownload = _dbContext.ProjectDownloads.Where(i => i.Id.ToString() == id).FirstOrDefault();
            if (projectDownload != null)
            {
                _dbContext.ProjectDownloads.Remove(projectDownload);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteProjectDownload(List<string> ids)
        {
            var contacts = _dbContext.ProjectDownloads.Where(i => ids.Contains(i.Id.ToString())).ToList();
            if (contacts.Count > 0)
            {
                _dbContext.ProjectDownloads.RemoveRange(contacts);
                _dbContext.SaveChanges();
            }
        }
    }
}
