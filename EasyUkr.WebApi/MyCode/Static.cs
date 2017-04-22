using EasyUkr.DataAccessLayer.Contexts;
namespace EasyUkr.WebApi.MyCode
{
    
    public static class Static
    {
        public static EasyUkrDbContext Data = new EasyUkrDbContext();
        public static string GrammarPath=$"\\Content\\Grammar\\";
        public static string IconPath= $"Content\\Icons\\Сategories";
    }
}