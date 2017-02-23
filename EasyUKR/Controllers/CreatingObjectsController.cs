using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUKR.Models;
using EasyUKR.DB;
using EasyUKR.MyCode;
using static EasyUKR.Controllers.CmsController;

namespace EasyUKR.Controllers
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
                var path = AppDomain.CurrentDomain.BaseDirectory+'\\'+ $"{ MyCode.Static.IconPath}\\{ model.Translate}";
                Directory.CreateDirectory(path);
                //file.SaveAs(path + '\\' + model.File.FileName);
               

                Translate translate=new Translate();
                translate.ImageName = file.FileName;
                var buffer=new byte[file.ContentLength];
                file.InputStream.Read(buffer, 0, file.ContentLength);
                translate.ImageBytes = buffer;
               /* translate.ImageLink =
                    ($"{ MyCode.Static.IconPath}\\{model.Translate}\\" + model.File.FileName).Replace('\\', '/');*/
                translate.Translate1 = model.Translate.ToLower();

                Word word=new Word();
                word.Word1 = model.Header.ToLower();
                word.Translates.Add(translate);

                Topic newTopic = new Topic();
                newTopic.Header = model.Header.ToLower();
                newTopic.Words.Add(word);

                Static.data.Translates.Add(translate);
                Static.data.Words.Add(word);
                Static.data.Topics.Add(newTopic);
                Static.data.SaveChanges();

                //JavaScript("alert(\'fdsfdf\')");
                TempData["TopicSuccess"] = true;
                return RedirectToAction("TopicView","Cms");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteTopic(int id)
        {
            TempData["DelSuccess"] = false;
            var topic = Static.data.Topics.First(x => x.TopicID == id);
            foreach (var variable in topic.Words)
                Static.data.Translates.RemoveRange(variable.Translates);
            Static.data.Words.RemoveRange(topic.Words);
            Static.data.Topics.Remove(topic);
            Static.data.SaveChanges();
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

                var foundedTopic = Static.data.Topics?.First(x => x.TopicID == id);
                if (foundedTopic != null)
                {
                    var folder = foundedTopic.Words.First(x => x.Word1 == foundedTopic.Header).Translates.First().Translate1;

                    var file = model.Image;
                    var path = AppDomain.CurrentDomain.BaseDirectory+'\\'+ $"{MyCode.Static.IconPath}\\{folder}";
                    Directory.CreateDirectory(path);
                    //file.SaveAs(path + '\\' + model.Image.FileName); 


                    Translate translate=new Translate();
                    translate.ImageName = file.FileName;
                    var buffer = new byte[file.ContentLength];
                    file.InputStream.Read(buffer, 0, file.ContentLength);
                    translate.ImageBytes = buffer;
                    /*translate.ImageLink =
                        ($"{ MyCode.Static.IconPath}\\{folder}\\" + model.Image.FileName).Replace('\\', '/');*/
                    translate.Translate1 = model.Translate.ToLower();

                    Word word=new Word();
                    word.Word1 = model.Word.ToLower();
                    word.Translates.Add(translate);

                    foundedTopic.Words.Add(word);
                    Static.data.SaveChanges();

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
            var word = Static.data.Words.First(x => x.WID == id);
            Static.data.Translates.RemoveRange(word.Translates);
            int id1 = word.TopicID;
            Static.data.Words.Remove(word);
            Static.data.SaveChanges();
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
                var path = AppDomain.CurrentDomain.BaseDirectory + '\\' + MyCode.Static.GrammarPath;
                Directory.CreateDirectory(path);
                file.SaveAs(path + '\\' + model.File.FileName);

                GrammarTopic topic = new GrammarTopic();
                topic.Header = model.Header;
                topic.Translate = model.Translate;
                topic.File = model.File.FileName;
                Static.data.GrammarTopics.Add(topic);
                Static.data.SaveChanges();
                
                TempData["TopicSuccess"] = true;
                return RedirectToAction("GrammarView", "Cms");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteGrammar(int id)
        {
            TempData["DelSuccess"] = false;
            var topic = Static.data.GrammarTopics.First(x => x.id == id);
            foreach (var variable in topic.GrammarTasks)
                Static.data.GrammarAnswers.RemoveRange(variable.GrammarAnswers);
            Static.data.GrammarTasks.RemoveRange(topic.GrammarTasks);
            Static.data.GrammarTopics.Remove(topic);
            Static.data.SaveChanges();
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
            topic.GrammarTopic = Static.data.GrammarTopics.First(x => x.id == i.Value);
            Static.data.GrammarTasks.Add(topic);
            Static.data.SaveChanges();
            TempData["redirectedTopicId"] = i.Value;
            TempData["WordSuccess"] = true;
            return View();
        }
        [HttpGet]
        public ActionResult DeleteTask(int id)
        {
            TempData["DelSuccess"] = false;
            var task = Static.data.GrammarTasks.First(x => x.id == id);
            int idtopic = task.idTopic.Value;
            Static.data.GrammarAnswers.RemoveRange(task.GrammarAnswers);
            Static.data.GrammarTasks.Remove(task);
            Static.data.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("GrammarTasksView", "Cms",new {id= idtopic });
        }
        #endregion


        [HttpGet]
        public ActionResult EditTask(int id) //створити сторнку EEditTask
        {
            return View();
        }
    }
}
 