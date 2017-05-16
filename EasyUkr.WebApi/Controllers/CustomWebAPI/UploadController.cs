using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EasyUkr.WebApi.Infrastructure;
using EasyUkr.WebApi.Infrastructure.ExecutionStructure;
using EasyUkr.WebApi.Models;
using EasyUkr.WebApi.MyCode;
using Microsoft.AspNet.Identity;

namespace EasyUkr.WebApi.Controllers.CustomWebAPI
{
    [Authorize]
    public class UploadController : ApiController
    {
        private static string Name;

        public Task<IEnumerable<UploadedFile>> Post()
        {
            string folderName = "Content\\Icons";
            string path = HttpContext.Current.Server.MapPath("~\\" + folderName);
            string rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            Name = User.Identity.GetUserName();
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(path);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider)
                    .ContinueWith<IEnumerable<UploadedFile>>(t =>
                    {

                        if (t.IsCanceled)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }

                        var fileInfo = streamProvider.FileData.Select(i =>
                        {
                            var info = new FileInfo(i.LocalFileName);
                            var filePath = folderName + "\\" + info.Name;
                            lock (Name)
                            {
                                var user = DbManager.Instance.Data.Users.FirstOrDefault(x => x.UserName == Name);
                                if (user != null)
                                    user.Avatar = "~\\" + filePath;
                                DbManager.Instance.Data.SaveChanges();
                            }
                            return new UploadedFile(info.Name, rootUrl + '\\' + filePath, info.Length / 1024);
                        });
                        return fileInfo;
                    });
                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    "This request is not properly formatted"));
            }

        }
    }
}
