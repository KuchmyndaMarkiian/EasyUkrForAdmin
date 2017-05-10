namespace EasyUkr.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updategrammartask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrammarTasks", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GrammarTasks", "Description");
        }
    }
}
