using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EasyUkr.WebApi.Infrastructure;

namespace EasyUkr.WebApi.Controllers.Admin
{
    [Authorize(Roles = "AppAdmin")]
    public class AdminController : Controller
    {
        public ActionResult AdminPage()
        {
           return View();
        }

        public ActionResult TopicView()
        {
            return View(DbManager.Instance.Data.WordTopics.ToArray());
        }

        public ActionResult GrammarView()
        {
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

        public ActionResult RecommendationView()
        {
            return View(DbManager.Instance.Data.RecommendationCategories);
        }

        [HttpGet]
        public ActionResult RecommendationListView(int? id)
        {
            var tmp = DbManager.Instance.Data.Recommendations.Where(x => x.ParentCategory.Id == id).ToArray();
            if (tmp.Any())
                return View(tmp);
            return RedirectToAction("CreateRecommendation", "Recommendation", new {id = id});
        }

        public ActionResult DialogueView()
        {
            return View(DbManager.Instance.Data.Dialogues);
        }

        public ActionResult DialogueDetailsView(int id)
        {
            var tmp = DbManager.Instance.Data.Dialogues.FirstOrDefault(x => x.Id == id);
            if (tmp!=null)
                return View(tmp);
            return RedirectToAction("DialogueView", "Admin", new { id = id });
        }
    }
}
