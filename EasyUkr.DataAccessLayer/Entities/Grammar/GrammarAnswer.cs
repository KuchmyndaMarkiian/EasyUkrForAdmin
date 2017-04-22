using System.ComponentModel.DataAnnotations;

namespace EasyUkr.DataAccessLayer.Entities.Grammar
{
    public class GrammarAnswer
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool? IsCorrect { get; set; }
        [Required]
        public virtual GrammarTask GrammarTask { get; set; }
    }
}
