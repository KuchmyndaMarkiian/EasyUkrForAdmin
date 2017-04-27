using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EasyUkr.WebApi.MyCode;
using Microsoft.AspNet.Identity;

namespace EasyUkr.WebApi.Controllers
{
    public class CmsController : Controller
    {
        public ActionResult AdminPage()
        {
            string v = User.Identity.GetUserName();
            if (!string.IsNullOrEmpty(v))
            {
                var user = DbManager.Instance.Users.FirstOrDefault(x => x.UserName==v);
                if (user != null)
                    return View();
            }
            return HttpNotFound();
        }

        public ActionResult TopicView()
        {
            DbManager.Instance.SaveChanges();
            return View(DbManager.Instance.WordTopics.ToList());
        }

        public ActionResult GrammarView()
        {
            DbManager.Instance.SaveChanges();
            return View(DbManager.Instance.GrammarTopics.ToList());
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
            return View(DbManager.Instance.WordTopics.First(x => x.Id == id1).Words.ToList());
        }

        [HttpPost]
        public ActionResult DictionaryView()
        {
            return View(DbManager.Instance.WordTopics.First().Words.ToList());
        }

        [HttpPost]
        public ActionResult GrammarTasksView()
        {
            return View(DbManager.Instance.GrammarTopics.First().GrammarTasks.ToList());
        }

        [HttpGet]
        public ActionResult GrammarTasksView(int? id)
        {
            ViewData.Add(new KeyValuePair<string, object>("topic", id));
            return   View(DbManager.Instance.GrammarTopics.First(x => x.Id == id).GrammarTasks.ToList());
        }
    }
}
