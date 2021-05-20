using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignarRWebAPIDemo.AuthData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.Controllers.Authentication
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IAuthenticate authenticate;
        private ILogger<AuthenticateController> logger;
        public AuthenticateController(ILogger<AuthenticateController> logger, IAuthenticate authenticate)
        {
            this.logger = logger;
            this.authenticate = authenticate;
        }
        /// <summary>
        /// To authenticate user. Authenticated user can access token
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginModel login)
        {
            logger.LogInformation($"Login requested: {login.UserName}");
            var result = await authenticate.AuthenticateUser(login);
            if (result == null)
                return Unauthorized();
            logger.LogInformation($"Token for login {login.UserName} is generated {result.ToString()}");
            return Ok(result);
        }
    }
}
