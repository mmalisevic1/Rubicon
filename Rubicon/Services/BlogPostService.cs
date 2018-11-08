using AutoMapper;
using Rubicon.Data;
using Rubicon.Data.Tables;
using Rubicon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return Mapper.Map<BlogPost>(await _rubiconContext.BlogPosts.FirstOrDefaultAsync(f => f.Slug == slug));
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

        public async Task<BlogPost> UpdateBlogPost(string slug, BlogPost blogPost)
        {
            BlogPosts existingBlogPost = await _rubiconContext.BlogPosts.FirstOrDefaultAsync(f => f.Slug == slug);
            if (existingBlogPost == null) {
                throw new ArgumentException("There is no blog post for the provided slug.");
            }

            if (!string.IsNullOrEmpty(blogPost.Title))
            {
                existingBlogPost.Slug = SlugResolver.GetSlugFromTitle(blogPost.Title);
                existingBlogPost.Title = blogPost.Title;
            }
            if (!string.IsNullOrEmpty(blogPost.Description))
            {
                existingBlogPost.Description = blogPost.Description;
            }
            if (!string.IsNullOrEmpty(blogPost.Body))
            {
                existingBlogPost.Body = blogPost.Body;
            }
            existingBlogPost.UpdatedAt = DateTime.Now;

            await _rubiconContext.SaveChangesAsync();
            return Mapper.Map<BlogPost>(existingBlogPost);
        }

        public async Task DeleteBlogPost(string slug)
        {
            BlogPosts existingBlogPost = await _rubiconContext.BlogPosts.FirstOrDefaultAsync(f => f.Slug == slug);
            if (existingBlogPost == null) {
                throw new ArgumentException("There is no blog post for the provided slug.");
            }

            _rubiconContext.BlogPosts.Remove(existingBlogPost);

            await _rubiconContext.SaveChangesAsync();
        }
    }
}
