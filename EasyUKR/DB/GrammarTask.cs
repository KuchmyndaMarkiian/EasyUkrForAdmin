//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyUKR.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class GrammarTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GrammarTask()
        {
            this.GrammarAnswers = new HashSet<GrammarAnswer>();
        }
    
        public int id { get; set; }
        public string Description { get; set; }
        public string Translate { get; set; }
        public Nullable<int> idTopic { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GrammarAnswer> GrammarAnswers { get; set; }
        public virtual GrammarTopic GrammarTopic { get; set; }
    }
}
