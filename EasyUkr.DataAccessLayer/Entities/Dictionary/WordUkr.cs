using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyUkr.DataAccessLayer.Entities.Dictionary
{
    public class WordUkr
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Transcription { get; set; }
        public virtual ICollection<TranslateEng> Translates { get; set; }= new HashSet<TranslateEng>();
        [Required]
        public virtual WordTopic ParentTopic { get; set; }
    }
}
