using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using EasyUkr.DataAccessLayer.Entities;
using EasyUkr.DataAccessLayer.Entities.Dictionary;
using EasyUkr.DataAccessLayer.Entities.Grammar;
using EasyUkr.DataAccessLayer.Entities.Recomendation;
using EasyUkr.DataAccessLayer.Entities.User;
using EasyUkr.DataAccessLayer.Initializer;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EasyUkr.DataAccessLayer.Contexts
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
   public class EasyUkrDbContext : IdentityDbContext<User>
    {
        public EasyUkrDbContext()
            : base("name = EasyUkrNew", throwIfV1Schema: false)
        {
            Database.SetInitializer(new EasyUkrInitializer());

            try
            {
                if (Users.Any())
                {
                    var list = Users.Where(x => x.Level == null);
                    foreach (var user in list)
                    {
                        user.Level = Levels.FirstOrDefault(x => x.LevelHeader == LevelUkr.Beginner);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static EasyUkrDbContext Create()
        {
            return new EasyUkrDbContext();
        }

        #region Entities

        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<UserHistory> Histories { get; set; }
        public virtual DbSet<GrammarAnswer> GrammarAnswers { get; set; }
        public virtual DbSet<GrammarTask> GrammarTasks { get; set; }
        public virtual DbSet<GrammarTopic> GrammarTopics { get; set; }
        public virtual DbSet<TranslateEng> TranslateEngs { get; set; }
        public virtual DbSet<WordUkr> WordUkrs { get; set; }
        public virtual DbSet<WordTopic> WordTopics { get; set; }
        public virtual DbSet<Recomendation> Recomendations { get; set; }
        public virtual DbSet<RecomendationCategory> RecomendationCategories { get; set; }

        public enum LevelUkr
        {
            Beginner,
            PreIntermediate,
            Intermediate,
            UpperIntermediate,
            Advance,
            Pro
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
            modelBuilder.Entity<User>().HasOptional(u => u.Level).WithMany(l => l.Users);

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
        }

        
    }
}