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
    public class B_IntelInnovationProjectApplyQue
    {
        D_IntelInnovationProjectApplyQue _que = new D_IntelInnovationProjectApplyQue();

        public M_IntelInnovationProjectApplyQue GetIntelInnovationProjectApplyQueById(string id)
        {
            var intId = System.Convert.ToInt32(id);
            var que = _que.GetIntelInnovationProjectApplyQueById(intId);
            var result = ConverEntityToModel(que);
            return result;
        }

        public List<M_IntelInnovationProjectApplyQue> GetIntelInnovationProjectApplyQueByProjectId(string projectId)
        {
            var intId = System.Convert.ToInt32(projectId);
            var ques = _que.GetIntelInnovationProjectApplyQueByProjectId(intId);
            var result = ques.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        public List<M_IntelInnovationProjectApplyQue> GetAllData()
        {
            var ques = _que.GetAll();
            var result = ques.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="que"></param>
        /// <returns></returns>
        public List<M_IntelInnovationProjectApplyQue> GetPageData(M_IntelInnovationProjectApplyQue que)
        {
            IntelInnovationProjectApplyQue searchModel = new IntelInnovationProjectApplyQue()
            {
                IntelInnovationProjectApplyId = System.Convert.ToInt32(que.IntelInnovationProjectApplyId)
            };

            var offset = que.offset;
            var limit = que.limit;

            var pageResult = _que.GetPageData(searchModel, offset, limit);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(M_IntelInnovationProjectApplyQue que)
        {
            IntelInnovationProjectApplyQue searchModel = new IntelInnovationProjectApplyQue()
            {
                IntelInnovationProjectApplyId = System.Convert.ToInt32(que.IntelInnovationProjectApplyId)
            };

            var totalCount = _que.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        public M_IntelInnovationProjectApplyQue ConverEntityToModel(IntelInnovationProjectApplyQue que)
        {
            if (que == null) return null;

            var model = new M_IntelInnovationProjectApplyQue()
            {
                Id = que.Id.ToString(),
                Auditor = que.Auditor,
                AuditDate = que.AuditDate,
                AuditStatus = que.AuditStatus,
                AuditRemark = que.AuditRemark,
                Application = que.Application,
                ApplicationDate = que.ApplicationDate,
                Creator = que.Creator,
                CreateDate = que.CreateDate,
                IntelInnovationProjectApplyId = que.IntelInnovationProjectApplyId.ToString()

            };
            return model;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddIntelInnovationProjectApplyQue(M_IntelInnovationProjectApplyQue model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "新增成功";

            try
            {
                //新增项目
                var que = new IntelInnovationProjectApplyQue()
                {
                    Auditor = model.Auditor,
                    AuditDate = model.AuditDate,
                    AuditStatus = model.AuditStatus,
                    AuditRemark = model.AuditRemark,
                    Application = model.Application,
                    ApplicationDate = model.ApplicationDate,
                    Creator = model.Creator,
                    CreateDate = model.CreateDate,
                    IntelInnovationProjectApplyId = System.Convert.ToInt32(model.IntelInnovationProjectApplyId)
                };

                message.ReturnId = _que.AddIntelInnovationProjectApplyQue(que);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "新增失败，异常：" + e.Message;
            }
            return message;
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Message EditIntelInnovationProjectApplyQue(M_IntelInnovationProjectApplyQue model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "修改成功";

            try
            {
                var que = new IntelInnovationProjectApplyQue()
                {

                    Auditor = model.Auditor,
                    AuditDate = model.AuditDate,
                    AuditStatus = model.AuditStatus,
                    AuditRemark = model.AuditRemark
                };

                var id = System.Convert.ToInt32(model.Id);
                message.ReturnId = _que.EditIntelInnovationProjectApplyQue(id, que);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "修改失败，异常：" + e.Message;
            }
            return message;
        }

        public Message BatchUpdateAuditRemark(List<int> ids,string remark)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "更新成功";

            try
            {
                _que.BatchUpdateRemarkByIds(ids, remark);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "更新失败，异常：" + e.Message;
            }
            return message;
        }


        public Message DeleteById(string id)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "删除成功";

            try
            {
                var intId = System.Convert.ToInt32(id);
               
                _que.DeleteIntelInnovationProjectApplyQue(intId);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "删除失败，异常：" + e.Message;
            }
            return message;
        }

        public Message BatchDeleteByIds(List<string> ids)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "删除成功";

            try
            {
                //删除文件
               // _que.BatchDeleteIntelInnovationProjectApplyQue(ids);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "删除失败：" + e.Message;
            }

            return message;
        }


    
    }
}
