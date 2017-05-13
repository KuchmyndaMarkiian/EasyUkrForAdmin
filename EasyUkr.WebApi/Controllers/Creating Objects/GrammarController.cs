using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUkr.DataAccessLayer.Entities.Grammar;
using EasyUkr.WebApi.Models.ViewModels;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers.Creating_Objects
{
    public class GrammarController : Controller
    {
        #region GRAMMAR

        public ActionResult CreateGrammar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateGrammar(Topic model, HttpPostedFileBase uploadDoc)
        {
            TempData["TopicSuccess"] = false;
            if (ModelState.IsValid && uploadDoc != null)
            {
                model.File = uploadDoc;

                var file = model.File;
                var path = AppDomain.CurrentDomain.BaseDirectory + '\\' + Static.GrammarPath;
                Directory.CreateDirectory(path);
                file.SaveAs(path + '\\' + model.File.FileName);

                GrammarTopic topic = new GrammarTopic
                {
                    HeaderUkr = model.Header,
                    TranslateEng = model.Translate,
                    FileAdress = model.File.FileName
                };
                DbManager.Instance.Data.GrammarTopics.Add(topic);
                DbManager.Instance.Data.SaveChanges();

                TempData["TopicSuccess"] = true;
                return RedirectToAction("GrammarView", "Admin");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteGrammar(int id)
        {
            TempData["DelSuccess"] = false;
            var topic = DbManager.Instance.Data.GrammarTopics.First(x => x.Id == id);
            foreach (var variable in topic.GrammarTasks)
                DbManager.Instance.Data.GrammarAnswers.RemoveRange(variable.GrammarAnswers);
            DbManager.Instance.Data.GrammarTasks.RemoveRange(topic.GrammarTasks);
            DbManager.Instance.Data.GrammarTopics.Remove(topic);
            DbManager.Instance.Data.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("GrammarView", "Admin");
        }

        #endregion

        #region GRAMMAR TASK
        
        [HttpGet]
        public ActionResult CreateGrammarTask(int? id)
        {
            _answers = new List<GrammarAnswer>();
            var httpCookie = HttpContext.Response.Cookies["topic"];
            if (httpCookie != null)
                httpCookie.Value = id.Value.ToString();
            return View();
        }

        private static List<GrammarAnswer> _answers;
        [HttpPost]
        public ActionResult CreateGrammarTask(GrammarTask model,string submitEvent,string answerText,string answerCheck)
        {
            if (submitEvent == "Add" && !string.IsNullOrEmpty(answerText))
            {
                _answers.Add(new GrammarAnswer
                {
                    Answer = answerText,
                    IsCorrect = answerCheck == "on"
                });
                answerText = "";
                answerCheck = null;
                foreach (var vari in _answers)
                {
                    model.GrammarAnswers.Add(vari);
                }
            }
            else if(submitEvent == "Save")
            {
                TempData["TopicSuccess"] = false;
                var id = int.Parse(HttpContext.Request.Cookies["topic"].Value);
                if (!string.IsNullOrEmpty(model.Description) && !string.IsNullOrEmpty(model.HeaderUkr) && !string.IsNullOrEmpty(model.TranslateEng)
                        && _answers!=null && _answers.Any())
                //if (ModelState.IsValid)
                {
                    var foundedTopic =
                        DbManager.Instance.Data.GrammarTopics.FirstOrDefault(
                            x => x.Id == id);
                    if (foundedTopic != null)
                    {
                        model.GrammarAnswers = _answers;
                        GrammarTask topic = model;
                        foundedTopic.GrammarTasks.Add(topic);
                        DbManager.Instance.Data.GrammarTasks.Add(topic);
                        DbManager.Instance.Data.SaveChanges();
                        TempData["TopicSuccess"] = true;
                        return RedirectToAction("GrammarTasksView", "Admin", new {id = foundedTopic.Id});
                    }
                }
            }
            return View(model);
        }

        public ActionResult DeleteGrammarTask(int id)
        {
            TempData["DelSuccess"] = false;
            var topic = DbManager.Instance.Data.GrammarTasks.First(x => x.Id == id);
            int id1 = topic.GrammarTopic.Id;
            DbManager.Instance.Data.GrammarAnswers.RemoveRange(topic.GrammarAnswers);
            DbManager.Instance.Data.GrammarTasks.Remove(topic);
            DbManager.Instance.Data.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("GrammarTasksView", "Admin", new { id = id1 });
        }
        #endregion


    }
}