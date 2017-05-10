using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EasyUkr.DataAccessLayer.Entities.Recomendation;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers.Admin
{
    public class AdminController : Controller
    {
        public ActionResult AdminPage()
        {
            /*string v = User.Identity.GetUserName();
            if (!string.IsNullOrEmpty(v))
            {
                var user = DbManager.Instance.Data.Users.FirstOrDefault(x => x.UserName==v);
                if (user != null)
                    return View();
            }
            return HttpNotFound();*/
            return View();
        }

        public ActionResult TopicView()
        {
            DbManager.Instance.SaveChanges();
            return View(DbManager.Instance.Data.WordTopics.ToList());
        }

        public ActionResult GrammarView()
        {
            DbManager.Instance.SaveChanges();
            return View(DbManager.Instance.Data.GrammarTopics.ToList());
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
            return View(DbManager.Instance.Data.WordTopics.First(x => x.Id == id1).Words.ToList());
        }

        [HttpPost]
        public ActionResult DictionaryView()
        {
            return View(DbManager.Instance.Data.WordTopics.First().Words.ToList());
        }

        [HttpPost]
        public ActionResult GrammarTasksView()
        {
            return View(DbManager.Instance.Data.GrammarTopics.First().GrammarTasks);
        }

        [HttpGet]
        public ActionResult GrammarTasksView(int? id)
        {
            ViewData.Add(new KeyValuePair<string, object>("topic", id));
            var tmp = DbManager.Instance.Data.GrammarTopics.First(x => x.Id == id).GrammarTasks;
            if (tmp.Any())
                return View(tmp);
            return RedirectToAction("CreateGrammarTask", "Grammar", new { id = id });
        }

        public ActionResult RecomendationView()
        {
            return View(DbManager.Instance.Data.RecomendationCategories);
        }

        [HttpGet]
        public ActionResult RecomendationListView(int? id)
        {
            var tmp = DbManager.Instance.Data.Recomendations.Where(x => x.ParentCategory.Id == id).ToArray();
            if (tmp.Any())
                return View(tmp);
            return RedirectToAction("CreateRecomendation", "Recomendation", new {id = id});
        }
    }
}
