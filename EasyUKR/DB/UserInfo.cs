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
    
    public partial class UserInfo
    {
        public int InfoID { get; set; }
        public int UsID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<System.DateTime> DateOfReg { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string E_mail { get; set; }
    
        public virtual UserAccount UserAccount { get; set; }
    }
}
