using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace EasyUKR.Models
{
    public class TopicExt
    {
        public string Header { get; set; }
        public string Translate { get; set; }
        public HttpPostedFileBase File { get; set; }
    }

    public class WordExt
    {
        public string Word { get; set; }
        public string Translate { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }

    public struct Answer
    {
        private bool IsCorrected;
        private string Content;

        public Answer(string content, bool isCorrected)
        {
            Content = content;
            IsCorrected = isCorrected;
        }

        public Answer(string content)
        {
            this = new Answer(content, false);
        }

        public string GetContent() => Content;
        public bool GetAnswer() => IsCorrected;
    }
}