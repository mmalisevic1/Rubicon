using AutoMapper;
using Rubicon.Data.Tables;
using Rubicon.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Rubicon.Services
{
    public class MappingConfigurations
    {
        static bool initialized = false;

        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(Assembly.GetAssembly(typeof(MappingConfigurations)));
            });
            initialized = true;
        }
    }

    public class BlogPostsToBlogPost : Profile
    {
        public BlogPostsToBlogPost()
        {
            CreateMap<BlogPosts, BlogPost>()
                .ForMember(d => d.TagList, opt => opt.ResolveUsing(resolver => GetTagsInArray(resolver.Tags)));
        }

        private static string[] GetTagsInArray(IEnumerable<Tags> tags)
        {
            string[] tagArray = new string[tags.Count()];
            int i = 0;
            foreach (var tag in tags)
            {
                tagArray[i++] = tag.Tag;
            }
            return tagArray;
        }
    }

    public class BlogPostToBlogPosts : Profile
    {
        public BlogPostToBlogPosts()
        {

            CreateMap<BlogPost, BlogPosts>()
                .ForMember(d => d.Slug, opt => opt.ResolveUsing<SlugResolver>())
                .ForMember(d => d.Tags, opt => opt.ResolveUsing(resolver => GetTagsFromArray(resolver.TagList,
                    SlugResolver.GetSlugFromTitle(resolver.Title))));
        }

        private static IEnumerable<Tags> GetTagsFromArray(string[] tagArray, string blogPostId)
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
