using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyUkr.WebApi.Infrastructure;
using EasyUkr.WebApi.Models.RestModels;
using EasyUkr.WebApi.MyCode;

namespace EasyUkr.WebApi.Controllers.CustomWebAPI
{
    [Authorize]
    [RoutePrefix("api/Receive")]
    public class ReceivingController : ApiController
    {
        // GET api/Recieve/UserInfo
        [Route("UserInfo")]
        public UserInfo GetUserInfo()
        {
            var name = User.Identity.Name;
            var founded = DbManager.Instance.Data.Users.FirstOrDefault(x => x.UserName == name);
            if (founded == null)
                return null;

            return new UserInfo
            {
                DateOfBirth = founded.DateOfBirth,
                Nickname = founded.UserName,
                Email = founded.Email,
                Level = founded.Level.Text,
                Name = founded.Name,
                Score = founded.Score,
                Surname = founded.Surname
            };
        }

        // GET api/Recieve/UserAvatar
        [Route("UserAvatar")]
        public HttpResponseMessage GetAvatar()
        {
            var name = User.Identity.Name;
            var founded = DbManager.Instance.Data.Users.FirstOrDefault(x => x.UserName == name);
            if (founded == null)
                return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);

            string path = AppDomain.CurrentDomain.BaseDirectory + founded.Avatar.TrimStart('~');
            return FileDownloader.PutFile(path);
        }

        // GET api/Recieve/Topics
        [Route("Topics"), AllowAnonymous]
        public List<Topic> GetTopicsResources()
        {
            return new List<Topic>(
                DbManager.Instance.Data.WordTopics.Select(
                    t => new Topic
                    {
                        Id = t.Id,
                        Text = t.Header,
                        Translate = t.Words.FirstOrDefault().Translates.FirstOrDefault().Text,
                        TranslateImageId = t.Words.FirstOrDefault().Translates.FirstOrDefault().Id
                    }));
        }

        // GET api/Recieve/Words
        [Route("Words"), AllowAnonymous]
        public List<Word> GetWordsResources()
        {
            return new List<Word>(
                DbManager.Instance.Data.WordUkrs.Select(
                    t => new Word
                    {
                        Id = t.Id,
                        Text = t.Text,
                        ParentId = t.ParentTopic.Id,
                        Translate = t.Translates.FirstOrDefault().Text,
                        TranslateImageId = t.Translates.FirstOrDefault().Id
                    }));
        }

        // GET api/Recieve/Grammars
        [Route("Grammars"), AllowAnonymous]
        public List<Grammar> GetGrammarsResources()
        {
            return new List<Grammar>(
                DbManager.Instance.Data.GrammarTopics.Select(
                    t => new Grammar
                    {
                        Id = t.Id,
                        Text = t.HeaderUkr,
                        Translate = t.TranslateEng
                    }));
        }

        // GET api/Recieve/GrammarTasks
        [Route("GrammarTasks"), AllowAnonymous]
        public List<GrammarTask> GetGrammarTasksResources()
        {
            return new List<GrammarTask>(
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
        }

         // GET api/Recieve/RecomendationCategories
         [Route("RecomendationCategories"), AllowAnonymous]
         public List<RecomendationCategory> GetRecomendationCategoriesResources()
         {
             return new List<RecomendationCategory>(
                 DbManager.Instance.Data.RecomendationCategories.Select(
                     t => new RecomendationCategory
                     {
                         Text = t.HeaderUkr,
                         Translate = t.TranslateEng,
                         Id = t.Id
                     }));
         }
        // GET api/Recieve/Recomendations
        [Route("Recomendations"), AllowAnonymous]
        public List<Recomendation> GetRecomendationsResources()
        {
            return new List<Recomendation>(
                DbManager.Instance.Data.Recomendations.Select(
                    t => new Recomendation
                    {
                        Text = t.HeaderUkr,
                        Translate = t.TranslateEng,
                        UrlLink = t.UrlLink,
                        ParentId = t.ParentCategory.Id
                    }));
        }

        // GET api/Recieve/GetFile?type={type}%id={id}
        [Route("GetFile"), AllowAnonymous]
        public HttpResponseMessage GetFile(string type, int id)
        {
            string main = AppDomain.CurrentDomain.BaseDirectory;
            string file = null;
            switch (type)
            {
                case "word":
                {
                    var entity = DbManager.Instance.Data.TranslateEngs.FirstOrDefault(x => x.Id == id);
                    if (entity != null)
                        file = main + entity.FileAdress.TrimStart('~');
                    break;
                }
                case "recomend":
                {
                    var entity = DbManager.Instance.Data.Recomendations.FirstOrDefault(x => x.Id == id);
                    if (entity != null)
                        file = main + entity.FileAdress.TrimStart('~');
                    break;
                }
                case "grammar":
                {
                    var entity = DbManager.Instance.Data.GrammarTopics.FirstOrDefault(x => x.Id == id);
                    if (entity != null)
                        file = main + "Content\\Grammar\\" + entity.FileAdress.TrimStart('~');
                    break;
                }
            }
            if (file != null)
            {
                return FileDownloader.PutFile(file);
            }
            return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
        }
    }
}