using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using EasyUkr.WebApi.Infrastructure;
using EasyUkr.WebApi.Models.RestModels;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers.CustomWebAPI
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/Transmit"),
     ValidateAntiForgeryToken]
    public class TramsmitingController : ApiController
    {
        // GET api/Transmit/Topics
        [System.Web.Http.Route("Topics"), System.Web.Http.AllowAnonymous,
         ResponseType(typeof(List<Topic>))]
        public async Task<IHttpActionResult> GetTopicsResources()
        {
            try
            {
                return Ok(await Task.Run(() =>
                {
                    var res = new List<Topic>(
                        DbManager.Instance.Data.WordTopics.Select(
                            t => new Topic
                            {
                                Id = t.Id,
                                Text = t.Header,
                                Translate = t.Words.FirstOrDefault().Translates.FirstOrDefault().Text,
                                TranslateImageId = t.Words.FirstOrDefault().Translates.FirstOrDefault().Id
                            }));
                    DbManager.Instance.Data.SaveChangesAsync();
                    return res;
                }));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/Transmit/Words
        [System.Web.Http.Route("Words"), System.Web.Http.AllowAnonymous,
         ResponseType(typeof(List<Word>))]
        public async Task<IHttpActionResult> GetWordsResources()
        {
            try
            {
                return Ok(await Task.Run(() =>
                {
                    var res = new List<Word>(
                        DbManager.Instance.Data.WordUkrs.Select(
                            t => new Word
                            {
                                Id = t.Id,
                                Text = t.Text,
                                ParentId = t.ParentTopic.Id,
                                Translate = t.Translates.FirstOrDefault().Text,
                                TranslateImageId = t.Translates.FirstOrDefault().Id
                            }));
                    DbManager.Instance.Data.SaveChangesAsync();
                    return res;
                }));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/Transmit/Grammars
        [System.Web.Http.Route("Grammars"), System.Web.Http.AllowAnonymous,
         ResponseType(typeof(List<Grammar>))]
        public async Task<IHttpActionResult> GetGrammarsResources()
        {
            try
            {
                return Ok(await Task.Run(() =>
                {
                    var res = new List<Grammar>(
                        DbManager.Instance.Data.GrammarTopics.Select(
                            t => new Grammar
                            {
                                Id = t.Id,
                                Text = t.HeaderUkr,
                                Translate = t.TranslateEng
                            }));
                    DbManager.Instance.Data.SaveChangesAsync();
                    return res;
                }));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/Transmit/GrammarTasks
        [System.Web.Http.Route("GrammarTasks"), System.Web.Http.AllowAnonymous,
         ResponseType(typeof(List<GrammarTask>))]
        public async Task<IHttpActionResult> GetGrammarTasksResources()
        {
            try
            {
                return Ok(await Task.Run(() =>
                {
                    var res = new List<GrammarTask>(
                        DbManager.Instance.Data.GrammarTasks.Select(
                            t => new GrammarTask
                            {
                                Text = t.HeaderUkr,
                                Translate = t.TranslateEng,
                                Description = t.Description,
                                Answers = t.GrammarAnswers
                                    .Select(x => new GrammarAnswer {IsCorrect = x.IsCorrect.Value, Text = x.Answer})
                                    .ToList()
                            }));
                    DbManager.Instance.Data.SaveChangesAsync();
                    return res;
                }));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/Transmit/RecommendationCategories
        [System.Web.Http.Route("RecommendationCategories"), System.Web.Http.AllowAnonymous,
         ResponseType(typeof(List<RecommendationCategory>))]
        public async Task<IHttpActionResult> GetRecomendationCategoriesResources()
        {
            try
            {
                return Ok(await Task.Run(() =>
                {
                    var res = new List<RecommendationCategory>(
                        DbManager.Instance.Data.RecommendationCategories.Select(
                            t => new RecommendationCategory
                            {
                                Text = t.HeaderUkr,
                                Translate = t.TranslateEng,
                                Id = t.Id
                            }));
                    DbManager.Instance.Data.SaveChangesAsync();
                    return res;
                }));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/Transmit/Recommendations
        [System.Web.Http.Route("Recommendations"), System.Web.Http.AllowAnonymous,
         ResponseType(typeof(List<Recommendation>))]
        public async Task<IHttpActionResult> GetRecomendationsResources()
        {
            try
            {
                return Ok(await Task.Run(() =>
                {
                    var res = new List<Recommendation>(
                        DbManager.Instance.Data.Recommendations.Select(
                            t => new Recommendation
                            {
                                Id = t.Id,
                                Text = t.HeaderUkr,
                                Translate = t.TranslateEng,
                                UrlLink = t.UrlLink,
                                ParentId = t.ParentCategory.Id
                            }));
                    DbManager.Instance.Data.SaveChangesAsync();
                    return res;
                }));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/Transmit/Dialogues
        [System.Web.Http.Route("Dialogues"), System.Web.Http.AllowAnonymous,
         ResponseType(typeof(List<Dialogue>))]
        public async Task<IHttpActionResult> GetDialogues()
        {
            try
            {
                return Ok(await Task.Run(() =>
                {
                    var res = new List<Dialogue>(
                        DbManager.Instance.Data.Dialogues.Select(
                            t => new Dialogue
                            {
                                Id = t.Id,
                                DialogueEng = t.DialogueEng,
                                DialogueUkr = t.DialogueUkr,
                                Header = t.Header
                            }));
                    DbManager.Instance.Data.SaveChangesAsync();
                    return res;
                }));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/Transmit/GetFile?type={type}%id={id}
        [System.Web.Http.Route("GetFile"), System.Web.Http.AllowAnonymous]
        public async Task<HttpResponseMessage> GetFile(string type, int id)
        {
            string main = AppDomain.CurrentDomain.BaseDirectory;
            string file = null;
            switch (type)
            {
                case "word":
                {
                    var entity = (await DbManager.Instance.Data.TranslateEngs.FirstOrDefaultAsync(x => x.Id == id));
                    if (entity != null)
                        file = main + entity.FileAdress.TrimStart('~');
                    break;
                }
                case "recommend":
                {
                    var entity = await DbManager.Instance.Data.Recommendations.FirstOrDefaultAsync(x => x.Id == id);
                    if (entity != null)
                        file = main + entity.FileAdress.TrimStart('~');
                    break;
                }
                case "recommendc":
                {
                    var entity =
                        await DbManager.Instance.Data.RecommendationCategories.FirstOrDefaultAsync(x => x.Id == id);
                    if (entity != null)
                        file = main + entity.FileAdress.TrimStart('~');
                    break;
                }
                case "grammar":
                {
                    var entity = await DbManager.Instance.Data.GrammarTopics.FirstOrDefaultAsync(x => x.Id == id);
                    if (entity != null)
                        file = main + "Content\\Grammar\\" + entity.FileAdress.TrimStart('~');
                    break;
                }
                case "dialogue":
                {
                    var entity = await DbManager.Instance.Data.Dialogues.FirstOrDefaultAsync(x => x.Id == id);
                    if (entity != null)
                        file = main + entity.FileAdress.TrimStart('~');
                    break;
                }
            }
            if (file != null)
            {
                return await FileDownloader.PutFile(file);
            }
            return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
        }
    }
}