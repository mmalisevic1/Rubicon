using AutoMapper;
using Rubicon.Data.Tables;
using Rubicon.Models;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Rubicon.Services
{
    public class SlugResolver : IValueResolver<BlogPost, BlogPosts, string>
    {
        public string Resolve(BlogPost source, BlogPosts destination, string destMember,
            ResolutionContext resolutionContext)
        {
            return GetSlugFromTitle(source.Title);
        }

        public static string RemoveDiacritics(string slug)
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

        public static string GetSlugFromTitle(string title)
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
            if (slug.Last() == '_')
            {
                slug = slug.Remove(slug.Length - 1, 1);
            }
            return RemoveDiacritics(slug);
        }
    }
}
