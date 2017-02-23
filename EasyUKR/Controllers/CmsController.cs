using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EasyUKR.DB;
using EasyUKR.MyCode;
using static System.Math;

namespace EasyUKR.Controllers
{
    public class CmsController : Controller
    {
        

        public ActionResult AdminPage()
        {
            return View();
        }

        public ActionResult TopicView()
        {
            Static.data.SaveChanges();
            return View(Static.data.Topics.ToList());
        }

        public ActionResult GrammarView()
        {
            Static.data.SaveChanges();
            return View(Static.data.GrammarTopics.ToList());
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
            return View(Static.data.Topics.First(x => x.TopicID == id1).Words.ToList());
        }

        [HttpPost]
        public ActionResult DictionaryView()
        {
            return View(Static.data.Topics.First().Words.ToList());
        }

        [HttpPost]
        public ActionResult GrammarTasksView()
        {
            return View(Static.data.GrammarTopics.First().GrammarTasks.ToList());
        }

        [HttpGet]
        public ActionResult GrammarTasksView(int? id)
        {
            ViewData.Add(new KeyValuePair<string, object>("topic", id));
            return   View(Static.data.GrammarTopics.First(x => x.id == id).GrammarTasks.ToList());
        }
    }
}
