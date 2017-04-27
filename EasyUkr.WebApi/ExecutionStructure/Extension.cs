using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyUkr.DataAccessLayer.Contexts;
using EasyUkr.DataAccessLayer.Entities.User;
using EasyUkr.WebApi.Models;
using EasyUkr.WebApi.MyCode;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EasyUkr.WebApi.ExecutionStructure
{
    public static class Extension
    {
        public static User ConvetToUser(this RegisterBindingModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.Nick,
                Email = model.Email,
                Score = 0,
                DateOfBirth = model.Date
            };
            return user;
        }
    }
}