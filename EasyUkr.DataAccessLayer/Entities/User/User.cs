using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EasyUkr.DataAccessLayer.Entities.User
{
    [Table("IdentityUser")]
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
        public virtual int? Score { get; set; }
        public virtual byte[] Avatar { get; set; }
        
        public virtual Level Level { get; set; }
        public virtual ICollection<UserHistory> Histories { get; set; } = new HashSet<UserHistory>();
        public virtual ICollection<Achievement> Achievements { get; set; } = new HashSet<Achievement>();
    }
}
