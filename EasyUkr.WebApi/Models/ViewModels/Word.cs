using System.Web;

namespace EasyUkr.WebApi.Models.ViewModels
{
    public class Word
    {
        public string Text { get; set; }
        public string Translate { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}