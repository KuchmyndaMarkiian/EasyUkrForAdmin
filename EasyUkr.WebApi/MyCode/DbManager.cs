using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyUkr.DataAccessLayer.Contexts;

namespace EasyUkr.WebApi.MyCode
{
    public class DbManager : EasyUkrDbContext
    {
        private static EasyUkrDbContext _data;
        private static object _syncRoot = new Object();

        protected DbManager()
        {
        }

        public static EasyUkrDbContext Instance
        {
            get
            {
                if (_data == null)
                {
                    lock (_syncRoot)
                    {
                        if (_data == null)
                        {
                            _data = new EasyUkrDbContext();
                        }
                    }
                }
                return _data;
            }
            set { throw new NotImplementedException(); }
        }
    }
}