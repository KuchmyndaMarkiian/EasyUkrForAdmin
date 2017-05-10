using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyUkr.DataAccessLayer.Entities.Grammar
{
    public class GrammarTask
    {
        public int Id { get; set; }
        public string HeaderUkr { get; set; }
        public string TranslateEng { get; set; }
        public string Description { get; set; }
        public virtual ICollection<GrammarAnswer> GrammarAnswers { get; set; } = new HashSet<GrammarAnswer>();
        [Required]
        public virtual GrammarTopic GrammarTopic { get; set; }
    }
}
