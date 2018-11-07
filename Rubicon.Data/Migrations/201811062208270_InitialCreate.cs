namespace Rubicon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Slug = c.String(nullable: false, maxLength: 300, unicode: false),
                        Title = c.String(nullable: false, maxLength: 300),
                        Description = c.String(nullable: false, maxLength: 1000),
                        Body = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Slug);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BlogPostId = c.String(nullable: false, maxLength: 300, unicode: false),
                        Tag = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPostId, cascadeDelete: true)
                .Index(t => t.BlogPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "BlogPostId", "dbo.BlogPosts");
            DropIndex("dbo.Tags", new[] { "BlogPostId" });
            DropTable("dbo.Tags");
            DropTable("dbo.BlogPosts");
        }
    }
}
