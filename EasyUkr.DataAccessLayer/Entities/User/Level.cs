using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyUkr.DataAccessLayer.Contexts;

namespace EasyUkr.DataAccessLayer.Entities.User
{
    public class Level
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public EasyUkrDbContext.LevelUkr LevelHeader { get; set; }
        public int MinScore { get; set; }
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
