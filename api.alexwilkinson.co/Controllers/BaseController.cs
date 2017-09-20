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
    public class BaseController : Controller
    {
        protected alexwilkinsonContext _context;
        protected BaseController()
        {
        }
    }
}
