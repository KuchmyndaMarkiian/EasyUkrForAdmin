using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUkr.DataAccessLayer.Entities.Dictionary;
using EasyUkr.WebApi.Models.ViewModels;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers.Creating_Objects
{
    [Authorize(Roles = "AppAdmin")]
    //TODO: Need reformat code to async methods #1
    public class DictionaryController:Controller
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
                var path = AppDomain.CurrentDomain.BaseDirectory + $"{Static.IconPath}\\{model.Translate}";
                Directory.CreateDirectory(path);
                var address = path + '\\' + file.FileName;
                file.SaveAs(address);


                TranslateEng translate = new TranslateEng
                {
                    FileAdress = $"~\\{Static.IconPath}\\{model.Translate}\\{file.FileName}",
                    Text = model.Translate.ToLower()
                };

                WordUkr word = new WordUkr { Text = model.Header.ToLower() };
                word.Translates.Add(translate);

                WordTopic newTopic = new WordTopic { Header = model.Header.ToLower() };
                newTopic.Words.Add(word);

                DbManager.Instance.Data.TranslateEngs.Add(translate);
                DbManager.Instance.Data.WordUkrs.Add(word);
                DbManager.Instance.Data.WordTopics.Add(newTopic);
                DbManager.Instance.Data.SaveChanges();
                TempData["TopicSuccess"] = true;
                return RedirectToAction("TopicView", "Admin");
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
                    var path = AppDomain.CurrentDomain.BaseDirectory + '\\' + $"{Static.IconPath}\\{folder}";
                    Directory.CreateDirectory(path);
                    file.SaveAs(path + '\\' + model.Image.FileName);


                    TranslateEng translate = new TranslateEng
                    {
                        FileAdress = $"~\\{Static.IconPath}\\{folder}\\{file.FileName}",
                        Text = model.Translate.ToLower()
                    };

                    WordUkr word = new WordUkr { Text = model.Text.ToLower() };
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
    }
}