namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Padmate.ServicePlatform.DataAccess.ServiceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Padmate.ServicePlatform.DataAccess.ServiceDbContext context)
        {
            //默认添加系统管理员用户
            #region Add Admin Role
            string adminRoleId = string.Empty;
            string admin = "SystemAdmin";
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == admin);
            if (adminRole == null)
            {
                var adminModel = new IdentityRole
                {
                    Name = admin
                };
                context.Roles.Add(adminModel);
                adminRoleId = adminModel.Id;
            }
            else
            {
                adminRoleId = adminRole.Id;
            }

            #endregion
            #region Init Admin User
            string userName = "SystemAdmin";
            string password = "admin123";
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                // Hash password
                var passwordHash = new PasswordHasher().HashPassword(password);

                var userModel = new ApplicationUser
                {
                    UserName = userName,
                    PasswordHash = passwordHash,
                    Email = "SystemAdmin@qq.com",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                context.Users.Add(userModel);
                //Add User to Admin
                var relation = new IdentityUserRole() { UserId = userModel.Id, RoleId = adminRoleId };
                userModel.Roles.Add(relation);
            }
            #endregion
            context.SaveChanges();
        }
    }
}
