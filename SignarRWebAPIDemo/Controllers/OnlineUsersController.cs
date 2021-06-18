using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignarRWebAPIDemo.Model;
using SignarRWebAPIDemo.MyHub;
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
        private readonly IHubContext<MessageHub> messageHub;
        private readonly ILogger<OnlineUsersController> logger;
        public OnlineUsersController(IHubContext<MessageHub> messageHub, ILogger<OnlineUsersController> logger)
        {
            this.messageHub = messageHub;
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

            //var list = HubUserData.connectedList;
            var l = HubUserData._connections.GetKeys().ToList();

            HubUserData.connectedList.Clear();
            foreach (var item in l)
            {
                HubUserData.connectedList.Add(item, new HubUserData() { UserName = item, Status = 1, UpdatedOn = DateTime.Now });
            }
            var list = HubUserData.connectedList.Select(x => x.Value);

            return Ok(list);

        }
    }
}
