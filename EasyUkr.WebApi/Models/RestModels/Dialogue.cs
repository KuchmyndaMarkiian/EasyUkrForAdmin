using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyUkr.WebApi.Models.RestModels
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string DialogueUkr { get; set; }
        public string DialogueEng { get; set; }
    }
}