using System.Linq;
using EasyUkr.DataAccessLayer.Contexts;
using EasyUkr.DataAccessLayer.Entities.User;
using EasyUkr.WebApi.Models;

namespace EasyUkr.WebApi.Infrastructure.ExecutionStructure
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