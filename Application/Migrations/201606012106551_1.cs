namespace Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.News", "Tags_Id", c => c.Int());
            CreateIndex("dbo.News", "Tags_Id");
            AddForeignKey("dbo.News", "Tags_Id", "dbo.Tags", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "Tags_Id", "dbo.Tags");
            DropIndex("dbo.News", new[] { "Tags_Id" });
            DropColumn("dbo.News", "Tags_Id");
            DropTable("dbo.Tags");
        }
    }
}
