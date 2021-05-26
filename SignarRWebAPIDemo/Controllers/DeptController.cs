using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignarRWebAPIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class DeptController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetList()
        {
            var list = Dept.list;
            return Ok(list);
        }
    }
}
