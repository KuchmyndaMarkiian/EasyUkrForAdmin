using System;
using System.Collections.Generic;

namespace EasyUkr.DataAccessLayer.Entities.Dictionary
{
    public class WordTopic
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public virtual ICollection<WordUkr> Words { get; set; } = new HashSet<WordUkr>();
    }
}
