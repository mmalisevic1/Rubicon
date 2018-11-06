using Rubicon.Models;
using System.Threading.Tasks;

namespace Rubicon.Services
{
    public interface IBlogPostService
    {
        Task<BlogPost> CreateBlogPost(BlogPost blogPost);
    }
}
