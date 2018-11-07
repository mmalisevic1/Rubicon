using AutoMapper;
using Rubicon.Data.Tables;
using Rubicon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rubicon.Services
{
    public class TagArrayResolver : IValueResolver<BlogPosts, BlogPost, string[]>
    {
        public string[] Resolve(BlogPosts source, BlogPost destination, string[] destMember, ResolutionContext context)
        {
            return CreateTagArrayFromTags(source.Tags);
        }

        public static string[] CreateTagArrayFromTags(IEnumerable<Tags> tags)
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
}
