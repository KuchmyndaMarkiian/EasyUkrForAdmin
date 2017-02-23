using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EasyUKR.DB;
namespace EasyUKR.Models
{
    public class Contex: DbContext
    {
        public DbSet<Topic> Topics { get; set; }
    }
}