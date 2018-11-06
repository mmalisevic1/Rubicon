using Rubicon.Data;
using Rubicon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Rubicon.Controllers
{
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
    }
}
