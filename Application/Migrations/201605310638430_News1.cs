namespace Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class News1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Content = c.String(),
                        CustomFileName = c.String(),
                        SavedFileName = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.News", new[] { "ApplicationUser_Id" });
            DropTable("dbo.News");
        }
    }
}
