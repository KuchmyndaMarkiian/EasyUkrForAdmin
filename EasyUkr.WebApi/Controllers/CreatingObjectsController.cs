using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUkr.DataAccessLayer.Entities.Dictionary;
using EasyUkr.DataAccessLayer.Entities.Grammar;
using EasyUkr.WebApi.Models;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers
{
    public class CreatingObjectsController : Controller
    {
        #region TOPIC
        public ActionResult CreateTopic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTopic(TopicExt model, HttpPostedFileBase uploadImage)
        {
            TempData["TopicSuccess"] = false;
            if (ModelState.IsValid && uploadImage != null)
            {
                model.File = uploadImage;
                
                var file = model.File;
                var path = AppDomain.CurrentDomain.BaseDirectory+'\\'+ $"{Static.IconPath}\\{model.Translate}";
                Directory.CreateDirectory(path);
                var address = path + '\\' + file.FileName;
                file.SaveAs(address);


                TranslateEng translate = new TranslateEng
                {
                    FileAdress = $"~\\{Static.IconPath}\\{model.Translate}\\{file.FileName}",
                    Text = model.Translate.ToLower()
                };
                /*var buffer=new byte[file.ContentLength];
file.InputStream.Read(buffer, 0, file.ContentLength);
translate.ImageBytes = buffer;*/
                /* translate.ImageLink =
                     ($"{ MyCode.Static.IconPath}\\{model.Translate}\\" + model.File.FileName).Replace('\\', '/');*/

                WordUkr word = new WordUkr {Text = model.Header.ToLower()};
                word.Translates.Add(translate);

                WordTopic newTopic = new WordTopic {Header = model.Header.ToLower()};
                newTopic.Words.Add(word);

                DbManager.Instance.TranslateEngs.Add(translate);
                DbManager.Instance.WordUkrs.Add(word);
                DbManager.Instance.WordTopics.Add(newTopic);
                DbManager.Instance.SaveChanges();
                TempData["TopicSuccess"] = true;
                return RedirectToAction("TopicView","Cms");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteTopic(int id)
        {
            TempData["DelSuccess"] = false;
            var topic = DbManager.Instance.WordTopics.First(x => x.Id == id);
            foreach (var variable in topic.Words)
                DbManager.Instance.TranslateEngs.RemoveRange(variable.Translates);
            DbManager.Instance.WordUkrs.RemoveRange(topic.Words);
            DbManager.Instance.WordTopics.Remove(topic);
            DbManager.Instance.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("TopicView", "Cms");
        }
        #endregion
        #region WORD
        [HttpPost]
        public ActionResult CreateWord(WordExt model, HttpPostedFileBase uploadImage)
        {
            TempData["WordSuccess"] = false;
            if (ModelState.IsValid && uploadImage != null)
            {
                int id = int.Parse(HttpContext.Request.Cookies["topic"].Value);

                model.Image = uploadImage;

                var foundedTopic = DbManager.Instance.WordTopics?.First(x => x.Id == id);
                if (foundedTopic != null)
                {
                    var folder = foundedTopic.Words.First(x => x.Text == foundedTopic.Header).Translates.First().Text;

                    var file = model.Image;
                    var path = AppDomain.CurrentDomain.BaseDirectory+'\\'+ $"{Static.IconPath}\\{folder}";
                    Directory.CreateDirectory(path);
                    file.SaveAs(path + '\\' + model.Image.FileName);


                    TranslateEng translate = new TranslateEng
                    {
                        FileAdress = $"~\\{Static.IconPath}\\{folder}\\{file.FileName}",
                        Text = model.Translate.ToLower()
                    };

                    WordUkr word = new WordUkr {Text = model.Word.ToLower()};
                    word.Translates.Add(translate);

                    foundedTopic.Words.Add(word);
                    DbManager.Instance.SaveChanges();

                    TempData["redirectedTopicId"] = id;
                    TempData["WordSuccess"] = true;
                    return RedirectToAction("DictionaryView", "Cms");
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult CreateWord(int? id)
        {
            var httpCookie = HttpContext.Response.Cookies["topic"];
            if (httpCookie != null)
                httpCookie.Value = id.Value.ToString();
            return View();
        }
        [HttpGet]
        public ActionResult DeleteWord(int id)
        {
            TempData["DelSuccess"] = false;
            var word = DbManager.Instance.WordUkrs.First(x => x.Id == id);
            DbManager.Instance.TranslateEngs.RemoveRange(word.Translates);
            int id1 = word.ParentTopic.Id;
            DbManager.Instance.WordUkrs.Remove(word);
            DbManager.Instance.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("DictionaryView", "Cms", new { id = id1 });
        }
        #endregion
        #region GRAMMAR
        public ActionResult CreateGrammar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGrammar(TopicExt model, HttpPostedFileBase uploadFile)
        {
            TempData["TopicSuccess"] = false;
            if (ModelState.IsValid && uploadFile != null)
            {
                model.File = uploadFile;

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
                DbManager.Instance.GrammarTopics.Add(topic);
                DbManager.Instance.SaveChanges();
                
                TempData["TopicSuccess"] = true;
                return RedirectToAction("GrammarView", "Cms");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteGrammar(int id)
        {
            TempData["DelSuccess"] = false;
            var topic = DbManager.Instance.GrammarTopics.First(x => x.Id == id);
            foreach (var variable in topic.GrammarTasks)
                DbManager.Instance.GrammarAnswers.RemoveRange(variable.GrammarAnswers);
            DbManager.Instance.GrammarTasks.RemoveRange(topic.GrammarTasks);
            DbManager.Instance.GrammarTopics.Remove(topic);
            DbManager.Instance.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("GrammarView", "Cms");
        }
        #endregion
        #region TASK
        
        [HttpGet]
        public ActionResult CreateTask(int? id)
        {
            TempData["grammTopic"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult CreateTask(GrammarTask topic)
        {
            TempData["WordSuccess"] = false;
            int? i = (int?) TempData["grammTopic"];
            topic.GrammarTopic = DbManager.Instance.GrammarTopics.First(x => x.Id == i.Value);
            DbManager.Instance.GrammarTasks.Add(topic);
            DbManager.Instance.SaveChanges();
            TempData["redirectedTopicId"] = i.Value;
            TempData["WordSuccess"] = true;
            return View();
        }
        [HttpGet]
        public ActionResult DeleteTask(int id)
        {
            TempData["DelSuccess"] = false;
            var task = DbManager.Instance.GrammarTasks.First(x => x.Id == id);
            int idtopic = task.GrammarTopic.Id;
            DbManager.Instance.GrammarAnswers.RemoveRange(task.GrammarAnswers);
            DbManager.Instance.GrammarTasks.Remove(task);
            DbManager.Instance.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("GrammarTasksView", "Cms",new {id= idtopic });
        }
        #endregion


        [HttpGet]
        public ActionResult EditTask(int id) // ToDo створити сторнку EEditTask
        {
            return View();
        }
    }
}
 