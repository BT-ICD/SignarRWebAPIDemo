using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignarRWebAPIDemo.MyHub;
using SignarRWebAPIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MessageHub> messageHub;
        public MessageController(IHubContext<MessageHub> messageHub)
        {
            this.messageHub= messageHub;
        }
        [HttpGet]
        public async Task<IActionResult> ServerTimeAsync()
        {
            SignarRWebAPIDemo.Model.ServerDateTime serverDateTime = new ServerDateTime();
            await messageHub.Clients.All.SendAsync("serverTime", "Server Time is:" + serverDateTime.CurrentDateTime.ToString() );
            return Ok();
        }
    }
}
