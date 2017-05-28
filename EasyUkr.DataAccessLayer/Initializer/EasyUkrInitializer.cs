using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyUkr.DataAccessLayer.Contexts;
using EasyUkr.DataAccessLayer.Entities.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EasyUkr.DataAccessLayer.Initializer
{
    public class EasyUkrInitializer : CreateDatabaseIfNotExists<EasyUkrDbContext>
    {
        readonly string _adminRoleName = "AppAdmin";
        readonly string _adminName;
        readonly string _adminPassword;

        public EasyUkrInitializer(string adminName, string adminPassword)
        {
            _adminName = adminName;
            _adminPassword = adminPassword;
        }

        private void InitializeRole(EasyUkrDbContext context)
        {
            if (!context.Users.Any())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<User>(new UserStore<User>(context));

                //Add new role "AppAdmin"
                var role = roleManager.FindByName(_adminRoleName);
                if (role == null)
                {
                    roleManager.Create(new IdentityRole(_adminRoleName));
                }

                //Add admin to db

                var user = userManager.FindByName(_adminName);
                if (user == null)
                {
                    var admin = new User
                    {
                        UserName = _adminName,
                        Email = "admin@admin.com",
                        Level = EasyUkrDbContext.LevelUkr.Beginner,
                        DateOfBirth = DateTime.Now
                    };
                    userManager.Create(admin, _adminPassword);
                    userManager.SetLockoutEnabled(admin.Id, false);
                    userManager.AddToRole(admin.Id, _adminRoleName);
                }
            }
        }

        protected override void Seed(EasyUkrDbContext context)
        {
           
            #region Constant Levels

            context.Levels.AddRange(new[]
            {
                new Level {MinScore = 0, Text = "Beginner", LevelHeader = EasyUkrDbContext.LevelUkr.Beginner},
                new Level
                {
                    MinScore = 75,
                    Text = "Pre Intermediate",
                    LevelHeader = EasyUkrDbContext.LevelUkr.PreIntermediate
                },
                new Level {MinScore = 200, Text = "Intermediate", LevelHeader = EasyUkrDbContext.LevelUkr.Intermediate},
                new Level
                {
                    MinScore = 500,
                    Text = "Upper Intermediate",
                    LevelHeader = EasyUkrDbContext.LevelUkr.UpperIntermediate
                },
                new Level {MinScore = 1000, Text = "Advance", LevelHeader = EasyUkrDbContext.LevelUkr.Advance},
                new Level {MinScore = 2000, Text = "Pro", LevelHeader = EasyUkrDbContext.LevelUkr.Pro}
            });
            context.SaveChanges();

            #endregion
            
            base.Seed(context);
            //Puting admin and set his role AppAdmin
            InitializeRole(context);
        }
    }
}
