namespace EasyUkr.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ImageAdress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Score = c.Int(),
                        Avatar = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Level_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Levels", t => t.Level_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Level_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Event = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        LevelHeader = c.Int(nullable: false),
                        MinScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.GrammarAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(),
                        IsCorrect = c.Boolean(),
                        GrammarTask_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrammarTasks", t => t.GrammarTask_Id, cascadeDelete: true)
                .Index(t => t.GrammarTask_Id);
            
            CreateTable(
                "dbo.GrammarTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderUkr = c.String(),
                        TranslateEng = c.String(),
                        GrammarTopic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrammarTopics", t => t.GrammarTopic_Id, cascadeDelete: true)
                .Index(t => t.GrammarTopic_Id);
            
            CreateTable(
                "dbo.GrammarTopics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderUkr = c.String(),
                        TranslateEng = c.String(),
                        FileAdress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecomendationCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderUkr = c.String(),
                        TranslateEng = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Recomendations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderUkr = c.String(),
                        TranslateEng = c.String(),
                        UrlLink = c.String(),
                        FileAdress = c.String(),
                        ParentCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecomendationCategories", t => t.ParentCategory_Id, cascadeDelete: true)
                .Index(t => t.ParentCategory_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TranslateEngs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsTopic = c.Boolean(nullable: false),
                        FileAdress = c.String(),
                        ParentWord_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WordUkrs", t => t.ParentWord_Id, cascadeDelete: true)
                .Index(t => t.ParentWord_Id);
            
            CreateTable(
                "dbo.WordUkrs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsTopic = c.Boolean(nullable: false),
                        ParentTopic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WordTopics", t => t.ParentTopic_Id, cascadeDelete: true)
                .Index(t => t.ParentTopic_Id);
            
            CreateTable(
                "dbo.WordTopics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserAndAchievements",
                c => new
                    {
                        Us = c.String(nullable: false, maxLength: 128),
                        Ach = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Us, t.Ach })
                .ForeignKey("dbo.AspNetUsers", t => t.Us, cascadeDelete: true)
                .ForeignKey("dbo.Achievements", t => t.Ach, cascadeDelete: true)
                .Index(t => t.Us)
                .Index(t => t.Ach);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TranslateEngs", "ParentWord_Id", "dbo.WordUkrs");
            DropForeignKey("dbo.WordUkrs", "ParentTopic_Id", "dbo.WordTopics");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Recomendations", "ParentCategory_Id", "dbo.RecomendationCategories");
            DropForeignKey("dbo.GrammarTasks", "GrammarTopic_Id", "dbo.GrammarTopics");
            DropForeignKey("dbo.GrammarAnswers", "GrammarTask_Id", "dbo.GrammarTasks");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Level_Id", "dbo.Levels");
            DropForeignKey("dbo.UserHistories", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAndAchievements", "Ach", "dbo.Achievements");
            DropForeignKey("dbo.UserAndAchievements", "Us", "dbo.AspNetUsers");
            DropIndex("dbo.UserAndAchievements", new[] { "Ach" });
            DropIndex("dbo.UserAndAchievements", new[] { "Us" });
            DropIndex("dbo.WordUkrs", new[] { "ParentTopic_Id" });
            DropIndex("dbo.TranslateEngs", new[] { "ParentWord_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Recomendations", new[] { "ParentCategory_Id" });
            DropIndex("dbo.GrammarTasks", new[] { "GrammarTopic_Id" });
            DropIndex("dbo.GrammarAnswers", new[] { "GrammarTask_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.UserHistories", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Level_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.UserAndAchievements");
            DropTable("dbo.WordTopics");
            DropTable("dbo.WordUkrs");
            DropTable("dbo.TranslateEngs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Recomendations");
            DropTable("dbo.RecomendationCategories");
            DropTable("dbo.GrammarTopics");
            DropTable("dbo.GrammarTasks");
            DropTable("dbo.GrammarAnswers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Levels");
            DropTable("dbo.UserHistories");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Achievements");
        }
    }
}
