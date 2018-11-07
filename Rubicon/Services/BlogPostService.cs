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
            BlogPosts createdBlogPost = _rubiconContext.BlogPosts.Add(Mapper.Map<BlogPosts>(blogPost));

            await _rubiconContext.SaveChangesAsync();
            return Mapper.Map<BlogPost>(createdBlogPost);
        }

        public async Task<BlogPost> GetBlogPostBySlug(string slug)
        {
            return Mapper.Map<BlogPost>(await _rubiconContext.BlogPosts.FindAsync(slug));
        }

        public IEnumerable<BlogPost> GetBlogPosts(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return Mapper.Map<IEnumerable<BlogPost>>(_rubiconContext.BlogPosts.OrderByDescending(o =>
                    o.UpdatedAt ?? o.CreatedAt));
            }
            return Mapper.Map<IEnumerable<BlogPost>>(_rubiconContext.BlogPosts.Where(w => w.Tags.Select(s => s.Tag)
                                                                                                .Contains(tag))
                                                                              .OrderByDescending(o =>
                                                                                o.UpdatedAt ?? o.CreatedAt));
        }
    }
}
