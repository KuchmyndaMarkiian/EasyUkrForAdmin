using System.Web;

namespace EasyUkr.WebApi.Models.ViewModels
{
    public class TopicWord
    {
        public string Header { get; set; }
        public string Transcription { get; set; }
        public string Translate { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}