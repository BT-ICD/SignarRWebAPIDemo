using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignarRWebAPIDemo.MyHub;
using SignarRWebAPIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SignarRWebAPIDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MessageHub> messageHub;
        private readonly ILogger<MessageController> logger;
        public MessageController(IHubContext<MessageHub> messageHub, ILogger<MessageController> logger)
        {
            this.messageHub= messageHub;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> ServerTimeAsync()
        {
            SignarRWebAPIDemo.Model.ServerDateTime serverDateTime = new ServerDateTime();
            await messageHub.Clients.All.SendAsync("serverTime", "Server Time is:" + serverDateTime.CurrentDateTime.ToString() );
            return Ok();
        }
        [HttpGet]
        [Route("{user}/{message}")]
        public async Task<IActionResult> SendMessageToUserAsync(string user, string message)
        {
            logger.LogInformation($"Send Message to {user}, {message}");
            await messageHub.Clients.User(user).SendAsync("onPrivateMessageReceived", message);
            return Ok();
        }
    }
}
