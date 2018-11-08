using System.Collections.Generic;

namespace Rubicon.Services
{
    public interface ITagService
    {
        IEnumerable<string> GetTags();
    }
}
