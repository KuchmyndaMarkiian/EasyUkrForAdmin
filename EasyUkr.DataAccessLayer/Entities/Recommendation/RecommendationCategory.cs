using System.Collections.Generic;

namespace EasyUkr.DataAccessLayer.Entities.Recommendation
{
    public class RecommendationCategory
    {
        public int Id { get; set; }
        public string HeaderUkr { get; set; }
        public string TranslateEng { get; set; }
        public string FileAdress { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; } = new HashSet<Recommendation>();
    }
}
