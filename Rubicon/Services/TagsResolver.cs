using AutoMapper;
using Rubicon.Data.Tables;
using Rubicon.Models;
using System.Collections.Generic;

namespace Rubicon.Services
{
    public class TagsResolver : IValueResolver<BlogPost, BlogPosts, ICollection<Tags>>
    {
        public ICollection<Tags> Resolve(BlogPost source, BlogPosts destination, ICollection<Tags> destMember, ResolutionContext context)
        {
            return CreateTagsFromTagArray(source.TagList, source.Slug);
        }

        public static ICollection<Tags> CreateTagsFromTagArray(string[] tagArray, string blogPostId)
        {
            List<Tags> tags = new List<Tags>();
            for (int i = 0; i < tagArray.Length; i++)
            {
                tags.Add(new Tags
                {
                    BlogPostId = blogPostId,
                    Tag = tagArray[i]
                });
            }
            return tags;
        }
    }
}
