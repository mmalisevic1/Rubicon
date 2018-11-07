using Rubicon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rubicon.Services
{
    public interface IBlogPostService
    {
        Task<BlogPost> CreateBlogPost(BlogPost blogPost);

        Task<BlogPost> GetBlogPostBySlug(string slug);

        IEnumerable<BlogPost> GetBlogPosts(string tag);

        Task<BlogPost> UpdateBlogPost(string slug, BlogPost blogPost);
    }
}
