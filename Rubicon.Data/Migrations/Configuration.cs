namespace Rubicon.Data.Migrations
{
    using Rubicon.Data.Tables;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Rubicon.Data.RubiconContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Rubicon.Data.RubiconContext";
        }

        protected override void Seed(RubiconContext context)
        {
            context.BlogPosts.AddOrUpdate(identifierExpression => identifierExpression.Slug,
                new BlogPosts
                {
                    Slug = "augmented_reality_ios_application",
                    Title = "Augmented Reality iOS Application",
                    Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    CreatedAt = new DateTime(2018, 5, 18, 3, 22, 56, 637),
                    UpdatedAt = new DateTime(2018, 5, 18, 3, 48, 35, 824),
                    Tags = new List<Tags>
                    {
                        new Tags { Tag = "iOS" },
                        new Tags { Tag = "AR" },
                    }
                },
                new BlogPosts
                {
                    Slug = "augmented_reality_ios_application_2",
                    Title = "Augmented Reality iOS Application 2",
                    Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    CreatedAt = new DateTime(2018, 6, 18, 3, 22, 56, 637),
                    Tags = new List<Tags>
                    {
                        new Tags { Tag = "iOS" },
                        new Tags { Tag = "AR" },
                        new Tags { Tag = "Gazzda" }
                    }
                });
        }
    }
}
