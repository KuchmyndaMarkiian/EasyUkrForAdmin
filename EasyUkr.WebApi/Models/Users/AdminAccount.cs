using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Web;
using EasyUkr.DataAccessLayer.Entities.User;

namespace EasyUkr.WebApi.Models.Users
{
    /// <summary>
    /// Singleton User
    /// </summary>
    public class AdminAccount : User
    {
        private static Lazy<AdminAccount> Instance = new Lazy<AdminAccount>(() => new User());

        private AdminAccount()
        {
        }

        public static User GetInstance() => Instance.IsValueCreated ? Instance.Value : null;
        public static void SetInstance(User user) => Instance = new Lazy<User>(()=> user);
    }
}