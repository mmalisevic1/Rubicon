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
            // Onlz used in case of creating Blog post with Tags
            // That is whz the blogPostId is 0
            return CreateTagsFromTagArray(source.TagList, 0);
        }

        public static ICollection<Tags> CreateTagsFromTagArray(string[] tagArray, long blogPostId)
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
