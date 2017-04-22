using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyUkr.DataAccessLayer.Entities.User
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImageAdress { get; set; }
        public virtual ICollection<User> GainedUsers { get; set; }= new HashSet<User>();
    }
}
