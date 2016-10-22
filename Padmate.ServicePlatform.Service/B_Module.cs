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
    public class B_Module
    {
        D_Module _dModule = new D_Module();

        public B_Module()
        {

        }

        M_User _currentUser;
        string _mapPath;
        public B_Module(M_User currentUser)
        {
            _currentUser = currentUser;

        }

        public B_Module(M_User currentUser,string mapPath)
        {
            _currentUser = currentUser;
            _mapPath = mapPath;

        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public List<M_Module> GetPageData(M_Module module)
        {
            Module searchModel = new Module() {
                Type = module.Type,
                SubTitle = module.SubTitle

            };

            var offset = module.offset;
            var limit = module.limit;


            var pageResult = _dModule.GetPageData(searchModel, offset, limit);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(M_Module module)
        {
            Module searchModel = new Module()
            {
                Type = module.Type,
                SubTitle = module.SubTitle
            };

            var totalCount = _dModule.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        

        public M_Module GetModuleById(int id)
        {
            B_Image bImage = new B_Image();

            var module = _dModule.GetModuleById(id);
            var result = ConverEntityToModel(module);
            return result;
        }

        public M_Module GetModuleByUrlId(string urlId)
        {
            B_Image bImage = new B_Image();

            var module = _dModule.GetModuleById(urlId);
            var result = ConverEntityToModel(module);
            return result;
        }


        public List<M_Module> GetModuleByType(string type)
        {

            var pageResult = _dModule.GetModulesByType(type,null);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="model"></param>
        /// 
        /// <returns></returns>
        public Message AddModule(M_Module model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "模块新增成功";

            try
            {
                //新增模块
                var module = new Module()
                {
                    Title = model.Title,
                    SubTitle = model.SubTitle,
                    Description = model.Description,
                    Content = model.Content,
                    CreateDate = DateTime.Now,
                    Creator = _currentUser.UserName,
                    Sequence = string.IsNullOrEmpty(model.Sequence)?0: System.Convert.ToInt32(model.Sequence),
                    Type = model.Type,
                    IsHref = model.IsHref,
                    Href = model.Href,
                    ModuleURLId = model.ModuleURLId
                };

                message.ReturnId = _dModule.AddModule(module);

            }catch(Exception e)
            {
                message.Success = false;
                message.Content = "模块新增失败，异常："+e.Message ;
            }
            return message;
        }

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Message EditModule(M_Module model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "模块修改成功";

            try
            {
                var module = new Module()
                {
                    Title = model.Title,
                    SubTitle = model.SubTitle,
                    Description = model.Description,
                    Content = model.Content,
                    CreateDate = DateTime.Now,
                    Creator = _currentUser.UserName,
                    Sequence = System.Convert.ToInt32(model.Sequence),
                    Type = model.Type,
                    IsHref = model.IsHref,
                    Href = model.Href,
                    ModuleURLId = model.ModuleURLId

                };

                message.ReturnId = _dModule.EditModule(model.Id, module);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "模块修改失败，异常：" + e.Message;
            }
            return message;
        }


        /// <summary>
        /// 修改模块图片id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Message UpdateImageId(int id,int imageId)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片更新成功";
            try
            {
                message.ReturnId = _dModule.EditImageId(id, imageId);

            }catch(Exception e)
            {
                message.Success = false;
                message.Content = "图片更新失败:"+e.Message;
            }

            return message;
        }


        public Message DeleteModule(int id)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "模块删除成功";
            try
            {
                B_Image bImage = new B_Image();
                //删除模块图标
                var module = _dModule.GetModuleById(id);
                if(module.ImageId != null)
                    bImage.DeleteImage(System.Convert.ToInt32(module.ImageId));

                _dModule.DeleteModule(id);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "模块删除失败，异常：" + e.Message;
            }
            return message;
        }

        public Message BatchDeleteByIds(List<int> ids)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "模块删除成功";

            try
            {
                _dModule.BatchDelete(ids);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "模块删除失败：" + e.Message;
            }

            return message;
        }

        private M_Module ConverEntityToModel(Module module)
        {
            if (module == null) return null;

            B_Image bImage = new B_Image();

            var model = new M_Module()
            {
                Id = module.Id,
                Title = module.Title,
                SubTitle = module.SubTitle,
                Description = module.Description,
                IsHref = module.IsHref,
                Href = module.Href,
                Content = module.Content,
                Image = module.ImageId == null ? null : bImage.GetImageById(System.Convert.ToInt32(module.ImageId)),
                Type = module.Type,
                Sequence = module.Sequence.ToString(),
                ModuleURLId = module.ModuleURLId
            };
            return model;
        }
    }
}
