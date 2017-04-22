using System.ComponentModel.DataAnnotations;

namespace EasyUkr.DataAccessLayer.Entities.User
{
    public class UserHistory
    {
        public int Id { get; set; }
        public string Event { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
