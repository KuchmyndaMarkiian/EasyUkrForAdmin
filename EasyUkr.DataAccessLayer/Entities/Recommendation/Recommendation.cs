using System.ComponentModel.DataAnnotations;

namespace EasyUkr.DataAccessLayer.Entities.Recommendation
{
    public class Recommendation
    {
        public int Id { get; set; }
        public string HeaderUkr { get; set; }
        public string TranslateEng { get; set; }
        public string UrlLink { get; set; }
        public string FileAdress { get; set; }
        [Required]
        public RecommendationCategory ParentCategory { get; set; }
    }
}
