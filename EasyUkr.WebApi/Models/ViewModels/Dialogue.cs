using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyUkr.WebApi.Models.ViewModels
{
    
    public class Dialogue
    {
        public string Header { get; set; }
        [DataType(DataType.MultilineText)]
        public string TextUkr { get; set; }
        [DataType(DataType.MultilineText)]
        public string TextEng { get; set; }
    }
}