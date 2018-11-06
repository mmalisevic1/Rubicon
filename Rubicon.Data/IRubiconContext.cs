using Rubicon.Data.Tables;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Rubicon.Data
{
    public interface IRubiconContext : IDisposable
    {
        DbSet<BlogPosts> BlogPosts { get; set; }

        DbSet<Tags> Tags { get; set; }

        Task<int> SaveChangesAsync();
    }
}
