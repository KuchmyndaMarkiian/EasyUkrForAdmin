using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers
{
    public class CmsController : Controller
    {
        public ActionResult AdminPage()
        {
            return View();
        }

        public ActionResult TopicView()
        {
            Static.Data.SaveChanges();
            return View(Static.Data.WordTopics.ToList());
        }

        public ActionResult GrammarView()
        {
            Static.Data.SaveChanges();
            return View(Static.Data.GrammarTopics.ToList());
        }

        [HttpGet]
        public ActionResult DictionaryView(int? id)
        {
            int id1;
            if (id != null)
                id1 = id.Value;
            else if (TempData["redirectedTopicId"] != null) id1 = (int) TempData["redirectedTopicId"];
            else
                return HttpNotFound();

            ViewData.Add(new KeyValuePair<string, object>("topic", id1));
            return View(Static.Data.WordTopics.First(x => x.Id == id1).Words.ToList());
        }

        [HttpPost]
        public ActionResult DictionaryView()
        {
            return View(Static.Data.WordTopics.First().Words.ToList());
        }

        [HttpPost]
        public ActionResult GrammarTasksView()
        {
            return View(Static.Data.GrammarTopics.First().GrammarTasks.ToList());
        }

        [HttpGet]
        public ActionResult GrammarTasksView(int? id)
        {
            ViewData.Add(new KeyValuePair<string, object>("topic", id));
            return   View(Static.Data.GrammarTopics.First(x => x.Id == id).GrammarTasks.ToList());
        }
    }
}
