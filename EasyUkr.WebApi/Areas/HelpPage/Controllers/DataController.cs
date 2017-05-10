using System.Web.Mvc;
using EasyUkr.WebApi.Infrastructure.ExecutionStructure;

namespace EasyUkr.WebApi.Areas.HelpPage.Controllers
{
    public class DataController : Controller
    {
        /// <summary>
        /// For downloading image from web site
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Upload()
        {
            Data.Refresh();
            Data.FromClient = false;
            foreach (string file in Request.Files)
            {
                var upload = Request.Files[file];
                if (upload != null)
                {
                    var stream = upload.InputStream;
                    var buffer = new byte[upload.ContentLength];
                    stream.Read(buffer, 0, upload.ContentLength);
                    Data.SerializeFile(buffer);
                }
            }
            return Json("файл загружен");
        }

    }
}