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
            List<Dept> list = new List<Dept>()
            {
                new Dept(){Id=10, Name="Accounting", Loc="Dallas"},
                new Dept(){Id=20, Name="Research", Loc="New Jersey"}
            };
            return Ok(list);
        }
    }
}
