using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyUkr.WebApi.Models.RestModels
{
    public class GrammarTask
    {
        public string Text { get; set; }
        public string Translate { get; set; }
        public string Description { get; set; }
        public ICollection<GrammarAnswer> Answers { get; set; }
    }

    public class GrammarAnswer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}