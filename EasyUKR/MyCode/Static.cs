using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyUKR.DB;

namespace EasyUKR.MyCode
{
    public static class Static
    {
        public static EasyUkrEntities1 data = new EasyUkrEntities1();
        public static string GrammarPath=$"\\Content\\Grammar\\";
        public static string IconPath= $"Content\\Icons\\categories";
    }
}