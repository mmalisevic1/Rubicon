using Rubicon.Data.Tables;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace Rubicon.Data
{
    public class RubiconContext : DbContext, IRubiconContext
    {
        public virtual DbSet<BlogPosts> BlogPosts { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties()
                .Having(h => h.GetCustomAttributes(false)
                              .OfType<NonUnicodeAttribute>()
                              .FirstOrDefault())
                .Configure((config, attribute) =>
                {
                    config.IsUnicode(false);
                });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
