using System.Web;

namespace EasyUkr.WebApi.Models.ViewModels
{
    public class Topic
    {
        public string Header { get; set; }
        public string Translate { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}