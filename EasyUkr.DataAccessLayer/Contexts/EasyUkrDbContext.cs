using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using EasyUkr.DataAccessLayer.Entities;
using EasyUkr.DataAccessLayer.Entities.Dialogue;
using EasyUkr.DataAccessLayer.Entities.Dictionary;
using EasyUkr.DataAccessLayer.Entities.Grammar;
using EasyUkr.DataAccessLayer.Entities.Recommendation;
using EasyUkr.DataAccessLayer.Entities.User;
using EasyUkr.DataAccessLayer.Initializer;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EasyUkr.DataAccessLayer.Contexts
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   public sealed class EasyUkrDbContext : IdentityDbContext<User>
    {
        public EasyUkrDbContext()
            : base("name = EasyUkrNew", throwIfV1Schema: false)
        {
            Database.SetInitializer(new EasyUkrInitializer("Admin",".EasyUkr17"));
        }

        static EasyUkrDbContext()
        {
            Database.SetInitializer(new EasyUkrInitializer("Admin", ".EasyUkr17"));
        }
        public static EasyUkrDbContext Create()
        {
            return new EasyUkrDbContext();
        }

        #region Entities
        
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<UserHistory> Histories { get; set; }
        public DbSet<GrammarAnswer> GrammarAnswers { get; set; }
        public DbSet<GrammarTask> GrammarTasks { get; set; }
        public DbSet<GrammarTopic> GrammarTopics { get; set; }
        public DbSet<TranslateEng> TranslateEngs { get; set; }
        public DbSet<WordUkr> WordUkrs { get; set; }
        public DbSet<WordTopic> WordTopics { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<RecommendationCategory> RecommendationCategories { get; set; }
       public DbSet<Dialogue> Dialogues { get; set; }


        public enum LevelUkr
        {
            Beginner=1,
            PreIntermediate=2,
            Intermediate=3,
            UpperIntermediate=4,
            Advance=5,
            Pro=6
        }

        #endregion
        /// <summary>
        /// Method which sets relationships witn entities using FluentAPI
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region User Part

            modelBuilder.Entity<UserHistory>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Level>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Achievement>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UserHistory>().HasRequired(u => u.User).WithMany(h => h.Histories);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Achievements)
                .WithMany(a => a.GainedUsers)
                .Map(maped =>
                {
                    maped.MapLeftKey("Us");
                    maped.MapRightKey("Ach");
                    maped.ToTable("UserAndAchievements");
                });

            #endregion

            #region Dictionary Part

            modelBuilder.Entity<TranslateEng>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Dialogue>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<WordUkr>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<WordTopic>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<WordUkr>().HasRequired(w => w.ParentTopic).WithMany(t => t.Words);
            modelBuilder.Entity<TranslateEng>().HasRequired(t => t.ParentWord).WithMany(w => w.Translates);

            #endregion

            #region Grammar Part
            
            modelBuilder.Entity<GrammarAnswer>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<GrammarTask>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<GrammarTopic>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<GrammarTask>().HasRequired(g => g.GrammarTopic).WithMany(t => t.GrammarTasks);
            modelBuilder.Entity<GrammarTask>().HasMany(g => g.GrammarAnswers).WithRequired(a => a.GrammarTask);

            #endregion
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("dbo.Users");
        }
    }
}