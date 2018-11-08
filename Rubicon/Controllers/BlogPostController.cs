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

        [HttpGet]
        [Route("posts")]
        public IHttpActionResult GetBlogPosts([FromUri]string tag = "")
        {
            return Ok(_blogPostService.GetBlogPosts(tag));
        }

        [HttpPatch]
        [HttpPut]
        [Route("posts/{slug}")]
        public async Task<IHttpActionResult> UpdateBlogPost(string slug, [FromBody]BlogPost blogPost)
        {
            if (ModelState.Keys.Contains("blogPost.Slug"))
            {
                ModelState["blogPost.Slug"].Errors.Clear();
            }
            if (ModelState.Keys.Contains("blogPost.Title") && ModelState["blogPost.Title"].Errors.Count < 2)
            {
                ModelState["blogPost.Title"].Errors.Clear();
            }
            if (ModelState.Keys.Contains("blogPost.Description") && ModelState["blogPost.Description"].Errors.Count < 2)
            {
                ModelState["blogPost.Description"].Errors.Clear();
            }
            if (ModelState.Keys.Contains("blogPost.Body"))
            {
                ModelState["blogPost.Body"].Errors.Clear();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await _blogPostService.UpdateBlogPost(slug, blogPost));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("posts/{slug}")]
        public async Task<IHttpActionResult> DeleteBlogPost(string slug)
        {
            try
            {
                await _blogPostService.DeleteBlogPost(slug);
                return Ok();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
