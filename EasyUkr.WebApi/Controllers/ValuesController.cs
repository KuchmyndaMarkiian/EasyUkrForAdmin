using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace EasyUkr.WebApi.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // // POST: api/Upload
        //public HttpResponseMessage Post()
        //{
        //    var result = new HttpResponseMessage(HttpStatusCode.OK);
        //    if (Request.Content.IsMimeMultipartContent())
        //    {
        //        Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
        //        {
        //            MultipartMemoryStreamProvider provider = task.Result;
        //            foreach (HttpContent content in provider.Contents)
        //            {
                        
        //                Stream stream = content.ReadAsStreamAsync().Result;
        //                Image image = Image.FromStream(stream);
                        
        //                    //var testName = content.Headers.ContentDisposition.Name;
        //                    String filePath = HostingEnvironment.MapPath("~/Content/UserAvatars/");
        //                    String[] headerValues = (String[])Request.Headers.GetValues("UniqueId");
        //                    String fileName = headerValues[0] + ".jpg";
        //                    String fullPath = Path.Combine(filePath, fileName);
        //                    image.Save(fullPath);
                        
        //            }
        //        });
        //        return result;
        //    }
        //    else
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
        //    }
        //}

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
