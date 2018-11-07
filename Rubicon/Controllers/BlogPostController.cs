using Rubicon.Data;
using Rubicon.Models;
using Rubicon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rubicon.Controllers
{
    [RoutePrefix("api")]
    public class BlogPostController : ApiController
    {
        IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        public BlogPostController()
        {
            _blogPostService = new BlogPostService(new RubiconContext());
        }

        [HttpPost]
        [Route("posts")]
        public async Task<IHttpActionResult> CreateBlogPost([FromBody]BlogPost blogPost)
        {
            if (ModelState.Keys.Contains("blogPost.Slug"))
            {
                ModelState["blogPost.Slug"].Errors.Clear();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (Request != null)
            {
                return Created(Request.RequestUri, await _blogPostService.CreateBlogPost(blogPost));
            }
            return Created("RubiconApi", await _blogPostService.CreateBlogPost(blogPost));
        }

        [HttpGet]
        [Route("posts/{slug}")]
        public async Task<IHttpActionResult> GetBlogPost(string slug)
        {
            return Ok(await _blogPostService.GetBlogPostBySlug(slug));
        }
    }
}
