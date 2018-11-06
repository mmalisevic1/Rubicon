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
        static readonly IList<char> punctuationMarks = new List<char>
        {
            '.', '?', '!', ',', ';', ':', '-', '(', ')', '[', ']', '{', '}', '"', '\''
        };

        public BlogPostToBlogPosts()
        {

            CreateMap<BlogPost, BlogPosts>()
                .ForMember(d => d.Slug, opt => opt.ResolveUsing(resolver => GetSlugFromTitle(resolver.Title)));
        }

        private static string GetSlugFromTitle(string title)
        {
            var titleChars = title.SkipWhile(s => char.IsPunctuation(s) || char.IsWhiteSpace(s));
            var stringBuilder = new StringBuilder();
            foreach (var letter in titleChars)
            {
                stringBuilder.Append(letter);
            }
            title = stringBuilder.ToString();
            string slug = "";
            bool underscoreSetted = false;
            foreach (var letter in title)
            {
                if (char.IsLetterOrDigit(letter))
                {
                    slug += char.ToLowerInvariant(letter);
                    underscoreSetted = false;
                }
                if (char.IsPunctuation(letter))
                {
                    continue;
                }
                if (char.IsWhiteSpace(letter) && !underscoreSetted)
                {
                    slug += '_';
                    underscoreSetted = true;
                }
            }
            return RemoveDiacritics(slug);
        }

        private static string RemoveDiacritics(string slug)
        {
            var normalizedString = slug.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var letter in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(letter);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(letter);
                }
            }
            return stringBuilder.ToString()
                                .Normalize(NormalizationForm.FormC);
        }
    }
}
