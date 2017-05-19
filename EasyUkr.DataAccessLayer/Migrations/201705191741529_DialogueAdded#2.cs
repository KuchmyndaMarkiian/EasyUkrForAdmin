namespace EasyUkr.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DialogueAdded2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dialogues", "ParentDialogueCategory_Id", "dbo.DialogueCategories");
            DropIndex("dbo.Dialogues", new[] { "ParentDialogueCategory_Id" });
            AddColumn("dbo.Dialogues", "Header", c => c.String());
            DropColumn("dbo.Dialogues", "ParentDialogueCategory_Id");
            DropTable("dbo.DialogueCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DialogueCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TextUkr = c.String(),
                        TextEng = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Dialogues", "ParentDialogueCategory_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Dialogues", "Header");
            CreateIndex("dbo.Dialogues", "ParentDialogueCategory_Id");
            AddForeignKey("dbo.Dialogues", "ParentDialogueCategory_Id", "dbo.DialogueCategories", "Id", cascadeDelete: true);
        }
    }
}
