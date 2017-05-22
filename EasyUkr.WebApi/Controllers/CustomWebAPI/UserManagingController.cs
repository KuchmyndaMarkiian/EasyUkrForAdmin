using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using EasyUkr.WebApi.Infrastructure;
using EasyUkr.WebApi.Models.RestModels;
using EasyUkr.WebApi.MyCode;
using Microsoft.AspNet.Identity;

namespace EasyUkr.WebApi.Controllers.CustomWebAPI
{
    /// <summary>
    /// Клас для роботи з даними користувача
    /// (завантаження, оновлення та оновлення аватарки)
    /// </summary>
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/UserManaging"),
     ValidateAntiForgeryToken]
    public class UserManagingController : ApiController
    {
        // GET api/UserManaging/UserInfo
        /// <summary>
        /// Завантаження даних користувача
        /// </summary>
        /// <returns>Результат у JSON</returns>
        [System.Web.Http.Route("UserInfo"),
         ResponseType(typeof(UserInfo))]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            try
            {
                return Ok(await Task.Run(() =>
                {
                    var name = User.Identity.Name;
                    var founded = DbManager.Instance.Data.Users.FirstOrDefault(x => x.UserName == name);
                    if (founded == null)
                        return null;
                    var dataLevels = DbManager.Instance.Data.Levels;
                    var nextLevelId = dataLevels.FirstOrDefault(x => x.LevelHeader == founded.Level)
                                          .Id + 1;
                    var nextScore = dataLevels.FirstOrDefault(x => x.Id == nextLevelId)?.MinScore;
                    var maxScoredLevel = dataLevels.FirstOrDefault(x => x.MinScore == dataLevels.Max(d => d.MinScore));
                    return new UserInfo
                    {
                        DateOfBirth = founded.DateOfBirth,
                        Nickname = founded.UserName,
                        Email = founded.Email,
                        Level =
                            DbManager.Instance.Data.Levels.FirstOrDefault(
                                    x => x.LevelHeader == founded.Level)
                                .Text,
                        Name = founded.Name,
                        Score = founded.Score,
                        MaxScore = nextScore.HasValue ? nextScore.Value : maxScoredLevel.MinScore,
                        Surname = founded.Surname,
                        IsTested = founded.IsTested
                    };
                }));
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
        // GET api/UserManaging/UserAvatar
        /// <summary>
        /// Заваниаження аватарки
        /// </summary>
        /// <returns>Результат файлом</returns>
        [System.Web.Http.Route("UserAvatar")]
        public async Task<HttpResponseMessage> GetAvatar()
        {
            var name = User.Identity.Name;
            var founded = await DbManager.Instance.Data.Users.FirstOrDefaultAsync(x => x.UserName == name);
            if (founded == null)
                return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);

            string path = AppDomain.CurrentDomain.BaseDirectory + founded.Avatar.TrimStart('~');
            return await FileDownloader.PutFile(path);
        }
        //POST api/UserManaging/UpdateUserInfo
        /// <summary>
        /// Оновлення даних коористувача
        /// </summary>
        /// <param name="info">Вхідні дані користувача</param>
        /// <returns>Результат</returns>
        [System.Web.Http.Route("UpdateUserInfo")]
        public async Task<IHttpActionResult> UpdateUserInfo(UserInfo info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Wrong data");
            }
            try
            {
                var name = User.Identity.Name;
                var founded = DbManager.Instance.Data.Users.FirstOrDefault(x => x.UserName == name);
                if (founded == null)
                    return null;
                founded.Name = info.Name;
                founded.Surname = info.Surname;
                founded.IsTested = info.IsTested;
                founded.Score = info.Score;
                var lvls = DbManager.Instance.Data.Levels;
                var lvl = await lvls.FirstOrDefaultAsync(x => x.LevelHeader == founded.Level);
                if (founded.Score >= lvl.MinScore)
                    founded.Level = (await lvls.FirstOrDefaultAsync(x => x.Id == lvl.Id + 1)).LevelHeader;
                await DbManager.Instance.Data.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new BadRequestErrorMessageResult(e.Message, this);
            }
            return Ok("Updated");
        }
    }
}
