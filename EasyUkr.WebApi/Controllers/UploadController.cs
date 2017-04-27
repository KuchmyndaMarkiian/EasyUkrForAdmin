using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using EasyUkr.WebApi.ExecutionStructure;
using EasyUkr.WebApi.Models;
using EasyUkr.WebApi.MyCode;
using Microsoft.AspNet.Identity;

namespace EasyUkr.WebApi.Controllers
{
    [Authorize]
    public class UploadController : ApiController
    {
        private static string Name;
        public Task<IEnumerable<MyFile>> Post()
        {
            try
            {
                string folderName = "Content/Icons";
                string path = HttpContext.Current.Server.MapPath("~/" + folderName);
                string rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
                Name = User.Identity.GetUserName();
                if (Request.Content.IsMimeMultipartContent())
                {
                    var streamProvider = new CustomMultipartFormDataStreamProvider(path);
                    var task = Request.Content.ReadAsMultipartAsync(streamProvider)
                        .ContinueWith<IEnumerable<MyFile>>(t =>
                        {

                            if (t.IsCanceled)
                            {
                                throw new HttpResponseException(HttpStatusCode.InternalServerError);
                            }

                            var fileInfo = streamProvider.FileData.Select(i =>
                            {
                                var info = new FileInfo(i.LocalFileName);
                                var filePath = folderName + "/" + info.Name;
                                lock (Name)
                                {
                                    var user = DbManager.Instance.Users.FirstOrDefault(x => x.UserName == Name);
                                    if (user != null)
                                        user.Avatar = "~/" + filePath;
                                    DbManager.Instance.SaveChanges();
                                }
                                return new MyFile(info.Name, rootUrl + '/' + filePath, info.Length / 1024);
                            });
                            return fileInfo;
                        });
                    //DbManager.Instance.SaveChanges();
                    return task;
                }
                else
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        "This request is not properly formatted"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
