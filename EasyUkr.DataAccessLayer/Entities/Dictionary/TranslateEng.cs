using System.ComponentModel.DataAnnotations;

namespace EasyUkr.DataAccessLayer.Entities.Dictionary
{
    public class TranslateEng
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsTopic { get; set; } = false;
        public string FileAdress { get; set; }

        [Required]
        public virtual WordUkr ParentWord { get; set; }
    }
}
