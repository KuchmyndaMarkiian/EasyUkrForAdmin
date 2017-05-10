using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyUkr.DataAccessLayer.Entities.Recomendation
{
    public class RecomendationCategory
    {
        public int Id { get; set; }
        public string HeaderUkr { get; set; }
        public string TranslateEng { get; set; }
        public string FileAdress { get; set; }
        public virtual ICollection<Recomendation> Recomendations { get; set; } = new HashSet<Recomendation>();
    }
}
