using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> logger;
        public TestController(ILogger<TestController> logger)
        {
            this.logger = logger;
        }
        [HttpGet]
        public IActionResult TestLog()
        {
            var data = new { value = "Hello Web API" };
            logger.LogInformation($"Hello Testlog {DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss")}");

            return Ok(data);
        }
    }
}
