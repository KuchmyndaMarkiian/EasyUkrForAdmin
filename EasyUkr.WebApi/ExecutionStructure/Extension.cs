using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyUkr.DataAccessLayer.Contexts;
using EasyUkr.DataAccessLayer.Entities.User;
using EasyUkr.WebApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EasyUkr.WebApi.ExecutionStructure
{
    public static class Extension
    {
        public static User ConvetToUser(this RegisterBindingModel model)
        {
            return new User
            {
                Level = new Level {MinScore = 0, Text = "Beginner", LevelHeader = EasyUkrDbContext.LevelUkr.Beginner},
                Avatar = Data.DesrializeFile(),
                Name = model.Name,
                Surname = model.Surname,
                UserName = model.Nick,
                Email = model.Email,
                Score = 0,
                DateOfBirth = model.Date
            };
        }
    }
}