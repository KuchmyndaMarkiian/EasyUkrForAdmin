using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyUKR.Controllers
{
    public class ExtensionController : Controller
    {
        // GET: Extension
        [HttpGet]
        public ActionResult OpenDoc(string file)
        {
            var stream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + '\\' + MyCode.Static.GrammarPath+"\\"+file);
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