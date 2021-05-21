using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignarRWebAPIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OnlineUsersController : ControllerBase
    {
        private readonly ILogger<OnlineUsersController> logger;
        public OnlineUsersController(ILogger<OnlineUsersController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var list = HubUserData.list;
            return Ok(list);
        }
        [HttpGet]
        public IActionResult GetConnected()
        {
            var list = HubUserData.connectedList;
            return Ok(list);

        }
    }
}
