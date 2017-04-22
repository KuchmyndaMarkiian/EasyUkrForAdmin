using System.Collections.Generic;
namespace EasyUkr.DataAccessLayer.Entities.Grammar
{
    public class GrammarTopic
    {
        public int Id { get; set; }
        public string HeaderUkr { get; set; }
        public string TranslateEng { get; set; }
        public string FileAdress { get; set; }
        public virtual ICollection<GrammarTask> GrammarTasks { get; set; } = new HashSet<GrammarTask>();
    }
}
