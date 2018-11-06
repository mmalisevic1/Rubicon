﻿using Rubicon.Data;
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
            var n = ModelState;
            ModelState["blogPost.Slug"].Errors.Clear();
            //ModelState["CreatedAt"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created(Request.RequestUri, await _blogPostService.CreateBlogPost(blogPost));
        }
    }
}
