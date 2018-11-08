using Rubicon.Data;
using Rubicon.Services;
using System.Web.Http;

namespace Rubicon.Controllers
{
    [RoutePrefix("api")]
    public class TagController : ApiController
    {
        ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public TagController()
        {
            _tagService = new TagService(new RubiconContext());
        }

        [HttpGet]
        [Route("tags")]
        public IHttpActionResult GetTags()
        {
            return Ok(_tagService.GetTags());
        }
    }
}
