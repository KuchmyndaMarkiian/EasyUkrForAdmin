using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyUkr.WebApi.Models.RestModels
{
    public class Recommendation
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Translate { get; set; }
        public string UrlLink { get; set; }
        public int ParentId { get; set; }
    }
}