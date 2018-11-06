using AutoMapper;
using Rubicon.Data;
using Rubicon.Data.Tables;
using Rubicon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubicon.Services
{
    public class BlogPostService : IBlogPostService
    {
        IRubiconContext _rubiconContext;

        public BlogPostService(IRubiconContext rubiconContext)
        {
            _rubiconContext = rubiconContext;
        }

        public async Task<BlogPost> CreateBlogPost(BlogPost blogPost)
        {
            blogPost.CreatedAt = DateTime.Now;
            blogPost.UpdatedAt = null;
            var n = Mapper.Map<BlogPost, BlogPosts>(blogPost);
            BlogPosts createdBlogPost = _rubiconContext.BlogPosts.Add(Mapper.Map<BlogPosts>(blogPost));

            await _rubiconContext.SaveChangesAsync();
            return Mapper.Map<BlogPost>(createdBlogPost);
        }
    }
}
