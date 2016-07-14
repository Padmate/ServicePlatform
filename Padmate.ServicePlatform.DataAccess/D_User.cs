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
            var user = _dbContext.Users.Where(u => u.Id == userid)
                .Select(u => new User() { 
                    Id = u.Id,
                    UserName = u.UserName,
                    PasswordHash = u.PasswordHash,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                }).FirstOrDefault();
            return user;
        }

        public User GetUserByName(string userName)
        {
           var user =  _dbContext.Users.Where(u=>u.UserName == userName)
               .Select(u => new User()
               {
                   Id = u.Id,
                   UserName = u.UserName,
                   PasswordHash = u.PasswordHash,
                   Email = u.Email,
                   PhoneNumber = u.PhoneNumber
               }).FirstOrDefault();
           return user;
        }
    }
}
