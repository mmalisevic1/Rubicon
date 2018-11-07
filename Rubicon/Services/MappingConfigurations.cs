using AutoMapper;
using Rubicon.Data.Tables;
using Rubicon.Models;
using System.Reflection;

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
                .ForMember(d => d.TagList, opt => opt.ResolveUsing<TagArrayResolver>());
        }
    }

    public class BlogPostToBlogPosts : Profile
    {
        public BlogPostToBlogPosts()
        {

            CreateMap<BlogPost, BlogPosts>()
                .ForMember(d => d.Slug, opt => opt.ResolveUsing<SlugResolver>())
                .ForMember(d => d.Tags, opt => opt.ResolveUsing<TagsResolver>());
        }
    }
}
