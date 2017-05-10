using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUkr.DataAccessLayer.Entities.Recomendation;
using EasyUkr.WebApi.Models.ViewModels;
using EasyUkr.WebApi.MyCode;
using Recomendation = EasyUkr.WebApi.Models.ViewModels.Recomendation;

namespace EasyUkr.WebApi.Controllers.Creating_Objects
{
    public class RecomendationController : Controller
    {
        #region RECOMENDATIONS

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
                    FileAdress = "~" + Static.RecomendationPath +
                                 model.Translate + "\\" + file.FileName
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
                    return RedirectToAction("RecomendationListView", "Admin", new { id = id });
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
    }
}