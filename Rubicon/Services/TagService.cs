using Rubicon.Data;
using System.Collections.Generic;
using System.Linq;

namespace Rubicon.Services
{
    public class TagService : ITagService
    {
        IRubiconContext _rubiconContext;

        public TagService(IRubiconContext rubiconContext)
        {
            _rubiconContext = rubiconContext;
        }

        public IEnumerable<string> GetTags()
        {
            List<string> uniqueTags = new List<string>();
            IEnumerable<string> tags = _rubiconContext.Tags.Select(s => s.Tag);
            foreach (var tag in tags)
            {
                if (!uniqueTags.Contains(tag))
                {
                    uniqueTags.Add(tag);
                }
            }
            return uniqueTags;
        }
    }
}
