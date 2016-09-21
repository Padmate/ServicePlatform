﻿using Microsoft.AspNet.Identity.EntityFramework;
using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_User
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        public User GetUserById(string userid)
        {
            var user = _dbContext.Users.Where(u => u.Id == userid).FirstOrDefault();
            var result = ConverApplicationUserToUser(user);
            return result;
        }

        public User GetUserByName(string userName)
        {
           var user =  _dbContext.Users.Where(u=>u.UserName == userName).FirstOrDefault();
           var result = ConverApplicationUserToUser(user);
           return result;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="user"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<User> GetPageData(User user, int skip, int limit)
        {
            var query = _dbContext.Users.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(user.UserName))
                query = query.Where(a => a.UserName.Contains(user.UserName));

            #endregion

            var applicationUser = query.OrderBy(r => r.UserName)
            .Skip(skip)
            .Take(limit)
            .ToList();

            var result = applicationUser
            .Select(r => ConverApplicationUserToUser(r))
                .ToList();

            return result;
        }

        public int GetPageDataTotalCount(User user)
        {
            var query = _dbContext.Users.Where(a => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(user.UserName))
                query = query.Where(a => a.UserName.Contains(user.UserName));

            #endregion

            var result = query.ToList().Count();

            return result;
        }

        public List<User> GetByMulitCondition(User searchModel)
        {
            var query = _dbContext.Users.Where(n => 1 == 1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(searchModel.Id))
                query = query.Where(a => a.Id == searchModel.Id);
            if (!string.IsNullOrEmpty(searchModel.UserName))
                query = query.Where(a => a.UserName == searchModel.UserName);
            #endregion

            var applicationUser = query
            .ToList();

            var result = applicationUser
            .Select(r => ConverApplicationUserToUser(r))
                .ToList();

            return result;
        }


        public List<User> GetAll()
        {
            var result = _dbContext.Users
                .Select(r => ConverApplicationUserToUser(r))
                .ToList();

            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string AddUser(User user)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            _dbContext.Users.Add(applicationUser);

            //添加角色
            if (user.Roles != null && user.Roles.Count > 0)
            {
                foreach (var role in user.Roles)
                {
                    IdentityUserRole userRole = new IdentityUserRole() { UserId = applicationUser.Id, RoleId = role.Id };
                    applicationUser.Roles.Add(userRole);

                }

            }
            _dbContext.SaveChanges();
            return applicationUser.Id;
        }

        public string EditUser(string id, User model)
        {
            var user = _dbContext.Users.FirstOrDefault(a => a.Id.ToString() == id);

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            //删除原来角色
            user.Roles.Clear();

            //添加角色
            if (model.Roles != null && model.Roles.Count > 0)
            {
                foreach (var role in model.Roles)
                {
                    IdentityUserRole userRole = new IdentityUserRole() { UserId = user.Id, RoleId = role.Id };
                    user.Roles.Add(userRole);

                }

            }

            _dbContext.SaveChanges();
            return user.Id.ToString();
        }



        public void DeleteUser(string id)
        {
            var user = _dbContext.Users.Where(i => i.Id == id).FirstOrDefault();
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public void BatchDeleteUser(List<string> ids)
        {
            foreach (var id in ids)
            {
                var user = _dbContext.Users.Where(i => i.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                }

            }
            _dbContext.SaveChanges();

        }

        private ApplicationUser ConverUserToApplicationUser(User user)
        {
            ApplicationUser result = new ApplicationUser();
            if (user != null)
            {
                result.Id = user.Id;
                result.UserName = user.UserName;
                result.PasswordHash = user.PasswordHash;
                result.Email = user.Email;
                result.PhoneNumber = user.PhoneNumber;
            }
            return result;
        }

        private User ConverApplicationUserToUser(ApplicationUser applicationUser)
        {
            User result = new User();
            D_Role dRole = new D_Role();
            var roles = new List<IdentityRole>();
            if(applicationUser.Roles.Count >0)
            {
                var roleIds = applicationUser.Roles.Select(ur=>ur.RoleId).ToList();
                roles = _dbContext.Roles.Where(r => roleIds.Contains(r.Id)).ToList();
            }

            if (applicationUser != null)
            {
                result.Id = applicationUser.Id;
                result.UserName = applicationUser.UserName;
                result.PasswordHash = applicationUser.PasswordHash;
                result.Email = applicationUser.Email;
                result.PhoneNumber = applicationUser.PhoneNumber;
                result.Roles = roles.Select(r => dRole.ConverIdentityRoleToRole(r)).ToList();
            }
            return result;
        }
    }
}
