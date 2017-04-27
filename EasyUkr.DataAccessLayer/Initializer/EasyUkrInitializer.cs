using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyUkr.DataAccessLayer.Contexts;
using EasyUkr.DataAccessLayer.Entities.User;

namespace EasyUkr.DataAccessLayer.Initializer
{
    public class EasyUkrInitializer : CreateDatabaseIfNotExists<EasyUkrDbContext>
    {
        protected override void Seed(EasyUkrDbContext context)
        {
            #region Constant Levels

            context.Levels.AddRange(new[]
            {
                new Level {MinScore = 0, Text = "Beginner"},
                new Level
                {
                    MinScore = 75,
                    Text = "Pre Intermediate"
                },
                new Level {MinScore = 200, Text = "Intermediate"},
                new Level
                {
                    MinScore = 500,
                    Text = "Upper Intermediate"
                },
                new Level {MinScore = 1000, Text = "Advance"},
                new Level {MinScore = 2000, Text = "Pro"}
            });
            context.SaveChanges();
            #endregion

            base.Seed(context);
        }
    }
}
