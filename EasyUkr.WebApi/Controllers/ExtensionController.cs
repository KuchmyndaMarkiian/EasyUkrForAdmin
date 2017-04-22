using System;
using System.IO;
using System.Web.Mvc;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers
{
    public class ExtensionController : Controller
    {
        // GET: Extension
        [HttpGet]
        public ActionResult OpenDoc(string file)
        {
            var stream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + '\\' + Static.GrammarPath+"\\"+file);
            return File(stream.BaseStream, $"{file}");
        }
        [HttpPost]
        public ActionResult OpenDoc()
        {
            return View();
        }

        public ActionResult FromByte(byte[] bytes)
        {
            return File(bytes, "image/png");
        }
    }
}