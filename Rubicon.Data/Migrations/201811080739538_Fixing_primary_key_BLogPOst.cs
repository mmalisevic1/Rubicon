namespace Rubicon.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixing_primary_key_BLogPOst : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "BlogPostId", "dbo.BlogPosts");
            DropIndex("dbo.Tags", new[] { "BlogPostId" });
            DropPrimaryKey("dbo.BlogPosts");
            AddColumn("dbo.BlogPosts", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Tags", "BlogPostId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.BlogPosts", "Id");
            CreateIndex("dbo.BlogPosts", "Slug", unique: true);
            CreateIndex("dbo.Tags", "BlogPostId");
            AddForeignKey("dbo.Tags", "BlogPostId", "dbo.BlogPosts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "BlogPostId", "dbo.BlogPosts");
            DropIndex("dbo.Tags", new[] { "BlogPostId" });
            DropIndex("dbo.BlogPosts", new[] { "Slug" });
            DropPrimaryKey("dbo.BlogPosts");
            AlterColumn("dbo.Tags", "BlogPostId", c => c.String(nullable: false, maxLength: 300, unicode: false));
            DropColumn("dbo.BlogPosts", "Id");
            AddPrimaryKey("dbo.BlogPosts", "Slug");
            CreateIndex("dbo.Tags", "BlogPostId");
            AddForeignKey("dbo.Tags", "BlogPostId", "dbo.BlogPosts", "Slug", cascadeDelete: true);
        }
    }
}
