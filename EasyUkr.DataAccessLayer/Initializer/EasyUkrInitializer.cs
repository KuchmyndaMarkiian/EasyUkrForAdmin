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
                new Level {MinScore = 0, Text = "Beginner",LevelHeader = EasyUkrDbContext.LevelUkr.Beginner},
                new Level
                {
                    MinScore = 75,
                    Text = "Pre Intermediate",LevelHeader = EasyUkrDbContext.LevelUkr.PreIntermediate
                },
                new Level {MinScore = 200, Text = "Intermediate",LevelHeader = EasyUkrDbContext.LevelUkr.Intermediate},
                new Level
                {
                    MinScore = 500,
                    Text = "Upper Intermediate"
                    ,LevelHeader = EasyUkrDbContext.LevelUkr.UpperIntermediate
                },
                new Level {MinScore = 1000, Text = "Advance",LevelHeader = EasyUkrDbContext.LevelUkr.Advance},
                new Level {MinScore = 2000, Text = "Pro",LevelHeader = EasyUkrDbContext.LevelUkr.Pro}
            });
            context.SaveChanges();
            #endregion

            base.Seed(context);
        }
    }
}
