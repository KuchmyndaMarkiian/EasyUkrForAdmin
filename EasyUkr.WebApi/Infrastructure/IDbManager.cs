using System;
using EasyUkr.DataAccessLayer.Contexts;

namespace EasyUkr.WebApi.Infrastructure
{
    public interface IDbManager:IDisposable
    {
        EasyUkrDbContext Data { get; }
 void SaveChanges();
    }
}