using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyUkr.DataAccessLayer.Entities.Dialogue
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string DialogueUkr { get; set; }
        public string DialogueEng { get; set; }
        public string FileAdress { get; set; }
    }
}
