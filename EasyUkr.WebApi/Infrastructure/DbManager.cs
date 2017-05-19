using System;
using EasyUkr.DataAccessLayer.Contexts;

namespace EasyUkr.WebApi.Infrastructure
{
#pragma warning disable S3881 // "IDisposable" should be implemented correctly
    public class DbManager : IDbManager
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    {
        private static DbManager _instance;
        private static readonly object _syncRoot = new Object();
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

        public void Dispose()
        {
            Data?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}