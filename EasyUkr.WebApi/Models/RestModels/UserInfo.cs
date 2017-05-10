using System;

namespace EasyUkr.WebApi.Models.RestModels
{
        //Get user account
        public class UserInfo
        {
            public string Name { get; set; }
            public string Nickname { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Email { get; set; }
            public int? Score { get; set; }
            public String Level { get; set; }
        }
    
}