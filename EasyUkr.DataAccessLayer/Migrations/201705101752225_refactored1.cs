namespace EasyUkr.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactored1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Recomendations", newName: "Recommendations");
            CreateTable(
                "dbo.RecommendationCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderUkr = c.String(),
                        TranslateEng = c.String(),
                        FileAdress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.RecomendationCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecomendationCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeaderUkr = c.String(),
                        TranslateEng = c.String(),
                        FileAdress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.RecommendationCategories");
            RenameTable(name: "dbo.Recommendations", newName: "Recomendations");
        }
    }
}
