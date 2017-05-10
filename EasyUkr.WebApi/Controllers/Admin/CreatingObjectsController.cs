using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUkr.DataAccessLayer.Entities.Dictionary;
using EasyUkr.DataAccessLayer.Entities.Grammar;
using EasyUkr.DataAccessLayer.Entities.Recomendation;
using EasyUkr.WebApi.Models.ViewModels;
using EasyUkr.WebApi.MyCode;
using Recomendation = EasyUkr.WebApi.Models.ViewModels.Recomendation;

namespace EasyUkr.WebApi.Controllers.Admin
{
    public class CreatingObjectsController : Controller
    {
        #region TOPIC
        public ActionResult CreateTopic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTopic(Topic model, HttpPostedFileBase uploadImage)
        {
            TempData["TopicSuccess"] = false;
            if (ModelState.IsValid && uploadImage != null)
            {
                model.File = uploadImage;
                
                var file = model.File;
                var path = AppDomain.CurrentDomain.BaseDirectory+ $"{Static.IconPath}\\{model.Translate}";
                Directory.CreateDirectory(path);
                var address = path + '\\' + file.FileName;
                file.SaveAs(address);


                TranslateEng translate = new TranslateEng
                {
                    FileAdress = $"~\\{Static.IconPath}\\{model.Translate}\\{file.FileName}",
                    Text = model.Translate.ToLower()
                };

                WordUkr word = new WordUkr {Text = model.Header.ToLower()};
                word.Translates.Add(translate);

                WordTopic newTopic = new WordTopic {Header = model.Header.ToLower()};
                newTopic.Words.Add(word);

                DbManager.Instance.Data.TranslateEngs.Add(translate);
                DbManager.Instance.Data.WordUkrs.Add(word);
                DbManager.Instance.Data.WordTopics.Add(newTopic);
                DbManager.Instance.Data.SaveChanges();
                TempData["TopicSuccess"] = true;
                return RedirectToAction("TopicView","Admin");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteTopic(int id)
        {
            TempData["DelSuccess"] = false;
            var topic = DbManager.Instance.Data.WordTopics.First(x => x.Id == id);
            foreach (var variable in topic.Words)
                DbManager.Instance.Data.TranslateEngs.RemoveRange(variable.Translates);
            DbManager.Instance.Data.WordUkrs.RemoveRange(topic.Words);
            DbManager.Instance.Data.WordTopics.Remove(topic);
            DbManager.Instance.Data.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("TopicView", "Admin");
        }
        #endregion
        #region WORD
        [HttpPost]
        public ActionResult CreateWord(Word model, HttpPostedFileBase uploadImage)
        {
            TempData["WordSuccess"] = false;
            if (ModelState.IsValid && uploadImage != null)
            {
                int id = int.Parse(HttpContext.Request.Cookies["topic"].Value);

                model.Image = uploadImage;

                var foundedTopic = DbManager.Instance.Data.WordTopics?.First(x => x.Id == id);
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

                    WordUkr word = new WordUkr {Text = model.Text.ToLower()};
                    word.Translates.Add(translate);

                    foundedTopic.Words.Add(word);
                    DbManager.Instance.Data.SaveChanges();

                    TempData["redirectedTopicId"] = id;
                    TempData["WordSuccess"] = true;
                    return RedirectToAction("DictionaryView", "Admin");
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
            var word = DbManager.Instance.Data.WordUkrs.First(x => x.Id == id);
            DbManager.Instance.Data.TranslateEngs.RemoveRange(word.Translates);
            int id1 = word.ParentTopic.Id;
            DbManager.Instance.Data.WordUkrs.Remove(word);
            DbManager.Instance.Data.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("DictionaryView", "Admin", new { id = id1 });
        }
        #endregion
        #region GRAMMAR
        public ActionResult CreateGrammar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGrammar(Topic model, HttpPostedFileBase uploadFile)
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
            topic.GrammarTopic = DbManager.Instance.Data.GrammarTopics.First(x => x.Id == i.Value);
            DbManager.Instance.Data.GrammarTasks.Add(topic);
            DbManager.Instance.Data.SaveChanges();
            TempData["redirectedTopicId"] = i.Value;
            TempData["WordSuccess"] = true;
            return View();
        }
        [HttpGet]
        public ActionResult DeleteTask(int id)
        {
            TempData["DelSuccess"] = false;
            var task = DbManager.Instance.Data.GrammarTasks.First(x => x.Id == id);
            int idtopic = task.GrammarTopic.Id;
            DbManager.Instance.Data.GrammarAnswers.RemoveRange(task.GrammarAnswers);
            DbManager.Instance.Data.GrammarTasks.Remove(task);
            DbManager.Instance.Data.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("GrammarTasksView", "Admin",new {id= idtopic });
        }
        [HttpGet]
        public ActionResult EditTask(int id) // ToDo створити сторнку EEditTask
        {
            return View();
        }
        #endregion

        #region Recomendations

        #region Category

        public ActionResult CreateRecomendationCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRecomendationCategory(Topic model, HttpPostedFileBase uploadFile)
        {
            TempData["TopicSuccess"] = false;
            if (ModelState.IsValid && uploadFile != null)
            {

                model.File = uploadFile;
                var file = model.File;
                var path = AppDomain.CurrentDomain.BaseDirectory + '\\' + Static.RecomendationPath + '\\' +
                           model.Translate;
                Directory.CreateDirectory(path);
                path += '\\' + model.File.FileName;
                file.SaveAs(path);

                RecomendationCategory recomendation = new RecomendationCategory
                {
                    HeaderUkr = model.Header,
                    TranslateEng = model.Translate,
                    FileAdress = "~"+Static.RecomendationPath +
                                 model.Translate+"\\"+file.FileName
                };
                DbManager.Instance.Data.RecomendationCategories.Add(recomendation);
                DbManager.Instance.Data.SaveChanges();

                TempData["TopicSuccess"] = true;
                return RedirectToAction("RecomendationView", "Admin");
            }

            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteRecomendationCategory(int id)
        {
            TempData["DelSuccess"] = false;
            var tag = DbManager.Instance.Data.RecomendationCategories.First(x => x.Id == id);
            DbManager.Instance.Data.Recomendations.RemoveRange(tag.Recomendations);
            int id1 = tag.Id;
            DbManager.Instance.Data.RecomendationCategories.Remove(tag);
            DbManager.Instance.Data.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("RecomendationView", "Admin", new { id = id1 });
        }

        #endregion

        #region Item
        [HttpGet]
        public ActionResult CreateRecomendation(int? id)
        {
            var httpCookie = HttpContext.Response.Cookies["topic"];
            if (httpCookie != null)
                httpCookie.Value = id.Value.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult CreateRecomendation(Recomendation model, HttpPostedFileBase uploadFile)
        {
            TempData["TopicSuccess"] = false;
            if (ModelState.IsValid && uploadFile != null)
            {
                int id = int.Parse(HttpContext.Request.Cookies["topic"].Value);
                var foundedCategory = DbManager.Instance.Data.RecomendationCategories.FirstOrDefault(x => x.Id == id);
                if (foundedCategory != null)
                {
                    var file = uploadFile;
                    var path = AppDomain.CurrentDomain.BaseDirectory + '\\' + Static.RecomendationPath + '\\' +
                               foundedCategory.TranslateEng;
                    Directory.CreateDirectory(path);
                    path += '\\' + file.FileName;
                    file.SaveAs(path);
                    var recomendation = new DataAccessLayer.Entities.Recomendation.Recomendation
                    {
                        HeaderUkr = model.HeaderUkr,
                        TranslateEng = model.TranslateEng,
                        FileAdress = "~" + Static.RecomendationPath +
                                     foundedCategory.TranslateEng + "\\" + file.FileName,
                        UrlLink = model.UrlLink,
                        ParentCategory = foundedCategory
                    };
                    foundedCategory.Recomendations.Add(recomendation);
                    DbManager.Instance.Data.Recomendations.Add(recomendation);

                    
                        DbManager.Instance.Data.SaveChanges();

                    TempData["redirectedTopicId"] = id;
                    TempData["WordSuccess"] = true;
                    return RedirectToAction("RecomendationListView", "Admin",new {id=id});
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteRecomendation(int id)
        {
            TempData["DelSuccess"] = false;
            var recomendation = DbManager.Instance.Data.Recomendations.FirstOrDefault(x => x.Id == id);
            if (recomendation != null)
            {
                int id1 = recomendation.ParentCategory.Id;
                DbManager.Instance.Data.Recomendations.Remove(recomendation);
                DbManager.Instance.Data.SaveChanges();
                TempData["DelSuccess"] = true;
                return RedirectToAction("RecomendationListView", "Admin", new { id = id1 });
            }
            return HttpNotFound();
        }

        #endregion

        #endregion


        #region GrammarTask

        public ActionResult CreateGrammarTask(int? id)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
 