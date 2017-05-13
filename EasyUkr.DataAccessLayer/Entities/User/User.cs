using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyUkr.DataAccessLayer.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EasyUkr.DataAccessLayer.Entities.User
{
    [Table("Users")]
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager,
            string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }

        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual int Score { get; set; } = 0;
        public virtual bool IsTested { get; set; } = false;
        public virtual string Avatar { get; set; }
        public virtual EasyUkrDbContext.LevelUkr Level { get; set; } = EasyUkrDbContext.LevelUkr.Beginner;
        public virtual ICollection<UserHistory> Histories { get; set; } = new HashSet<UserHistory>();
        public virtual ICollection<Achievement> Achievements { get; set; } = new HashSet<Achievement>();
    }
}
