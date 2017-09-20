using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.alexwilkinson.co.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class BlogPostController : BaseController
    {
        public BlogPostController(alexwilkinsonContext context)
        {
            _context = context;
        }

        // GET: api/blogpost
        [HttpGet]
        public IEnumerable<BlogPost> Get()
        {
            return _context.BlogPost.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
