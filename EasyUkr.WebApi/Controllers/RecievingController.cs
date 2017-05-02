using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EasyUkr.WebApi.Infrastructure;
using EasyUkr.WebApi.MyCode;
using static EasyUkr.WebApi.Models.ClientModels.ClientModels;

namespace EasyUkr.WebApi.Controllers
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/Receive")]
    public class ReceivingController : ApiController
    {
        // GET api/Recieve/UserInfo
        [System.Web.Http.Route("UserInfo")]
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
            return FileDownloader.PutImage(path);
        }

        // GET api/Recieve/Topics
        [System.Web.Http.Route("Topics"), System.Web.Http.AllowAnonymous]
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
        [System.Web.Http.Route("Words"), System.Web.Http.AllowAnonymous]
        public List<Word> GetWordsResources()
        {
            return new List<Word>(
                DbManager.Instance.Data.WordUkrs.Select(
                    t => new Word()
                    {
                        Id = t.Id,
                        Text = t.Text,
                        ParentId = t.ParentTopic.Id,
                        Translate = t.Translates.FirstOrDefault().Text,
                        TranslateImageId = t.Translates.FirstOrDefault().Id
                    }));
        }


        // GET api/Recieve/GetImage?type={type}%id={id}
        [System.Web.Http.Route("GetImage"), System.Web.Http.AllowAnonymous]
        public HttpResponseMessage GetImage(string type, int id)
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
                        file = main + entity.FileAdress.TrimStart('~');
                    break;
                }
            }
            if (file != null)
            {
                return FileDownloader.PutImage(file);
            }
            return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
        }
    }
}
