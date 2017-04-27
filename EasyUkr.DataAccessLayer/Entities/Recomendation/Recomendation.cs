using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyUkr.DataAccessLayer.Entities.Recomendation
{
    public class Recomendation
    {
        public int Id { get; set; }
        public string HeaderUkr { get; set; }
        public string TranslateEng { get; set; }
        public string UrlLink { get; set; }
        public string FileAdress { get; set; }
        [Required]
        public RecomendationCategory ParentCategory { get; set; }
    }
}
