using System;
using EasyUkr.DataAccessLayer.Contexts;

namespace EasyUkr.WebApi.Infrastructure
{
    public class DbManager
    {
        private static DbManager _instance;
        private static object _syncRoot = new Object();
        public EasyUkrDbContext Data { get; }

        protected DbManager()
        {
            Data=new EasyUkrDbContext();
        }

        public static DbManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        _instance = new DbManager();
                    }
                }
                return _instance;
            }
        }

        public void SaveChanges()
        {
            Data?.SaveChanges();
        }
    }
}