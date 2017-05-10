namespace EasyUkr.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecomendationCategories", "FileAdress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecomendationCategories", "FileAdress");
        }
    }
}
