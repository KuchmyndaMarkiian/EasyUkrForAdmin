using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUkr.DataAccessLayer.Entities.Recommendation;
using EasyUkr.WebApi.Models.ViewModels;
using EasyUkr.WebApi.MyCode;
using Recommendation = EasyUkr.WebApi.Models.ViewModels.Recommendation;

namespace EasyUkr.WebApi.Controllers.Creating_Objects
{
    [Authorize(Roles = "AppAdmin")]
    //TODO: Need reformat code to async methods #3
    public class RecommendationController : Controller
    {
        #region RECOMENDATIONS

        #region Category

        public ActionResult CreateRecommendationCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRecommendationCategory(Topic model, HttpPostedFileBase uploadFile)
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

                RecommendationCategory recomendation = new RecommendationCategory
                {
                    HeaderUkr = model.Header,
                    TranslateEng = model.Translate,
                    FileAdress = "~" + Static.RecomendationPath +
                                 model.Translate + "\\" + file.FileName
                };
                DbManager.Instance.Data.RecommendationCategories.Add(recomendation);
                DbManager.Instance.Data.SaveChanges();

                TempData["TopicSuccess"] = true;
                return RedirectToAction("RecommendationView", "Admin");
            }

            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteRecommendationCategory(int id)
        {
            TempData["DelSuccess"] = false;
            var tag = DbManager.Instance.Data.RecommendationCategories.First(x => x.Id == id);
            DbManager.Instance.Data.Recommendations.RemoveRange(tag.Recommendations);
            int id1 = tag.Id;
            DbManager.Instance.Data.RecommendationCategories.Remove(tag);
            DbManager.Instance.Data.SaveChanges();
            TempData["DelSuccess"] = true;
            return RedirectToAction("RecommendationView", "Admin", new { id = id1 });
        }

        #endregion

        #region Item
        [HttpGet]
        public ActionResult CreateRecommendation(int? id)
        {
            var httpCookie = HttpContext.Response.Cookies["topic"];
            if (httpCookie != null)
                httpCookie.Value = id.Value.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult CreateRecommendation(Recommendation model, HttpPostedFileBase uploadFile)
        {
            TempData["TopicSuccess"] = false;
            if (ModelState.IsValid && uploadFile != null)
            {
                int id = int.Parse(HttpContext.Request.Cookies["topic"].Value);
                var foundedCategory = DbManager.Instance.Data.RecommendationCategories.FirstOrDefault(x => x.Id == id);
                if (foundedCategory != null)
                {
                    var file = uploadFile;
                    var path = AppDomain.CurrentDomain.BaseDirectory + '\\' + Static.RecomendationPath + '\\' +
                               foundedCategory.TranslateEng;
                    Directory.CreateDirectory(path);
                    path += '\\' + file.FileName;
                    file.SaveAs(path);
                    var recomendation = new DataAccessLayer.Entities.Recommendation.Recommendation
                    {
                        HeaderUkr = model.HeaderUkr,
                        TranslateEng = model.TranslateEng,
                        FileAdress = "~" + Static.RecomendationPath +
                                     foundedCategory.TranslateEng + "\\" + file.FileName,
                        UrlLink = model.UrlLink,
                        ParentCategory = foundedCategory
                    };
                    foundedCategory.Recommendations.Add(recomendation);
                    DbManager.Instance.Data.Recommendations.Add(recomendation);


                    DbManager.Instance.Data.SaveChanges();

                    TempData["redirectedTopicId"] = id;
                    TempData["WordSuccess"] = true;
                    return RedirectToAction("RecommendationListView", "Admin", new { id = id });
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult DeleteRecommendation(int id)
        {
            TempData["DelSuccess"] = false;
            var recomendation = DbManager.Instance.Data.Recommendations.FirstOrDefault(x => x.Id == id);
            if (recomendation != null)
            {
                int id1 = recomendation.ParentCategory.Id;
                DbManager.Instance.Data.Recommendations.Remove(recomendation);
                DbManager.Instance.Data.SaveChanges();
                TempData["DelSuccess"] = true;
                return RedirectToAction("RecommendationListView", "Admin", new { id = id1 });
            }
            return HttpNotFound();
        }

        #endregion

        #endregion
    }
}