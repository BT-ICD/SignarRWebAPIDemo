using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignarRWebAPIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.MyHub
{
    [Authorize]
    public class MessageHub:Hub
    {
        private readonly ILogger<MessageHub> logger;
        public MessageHub(ILogger<MessageHub> logger)
        {
            this.logger = logger;
        }
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync()
        {
            var userName = this.Context.User.Identity.Name;
            var data = new HubUserData { UserName = userName, Status = 1, UpdatedOn = DateTime.Now };
            
            HubUserData.list.Add(data);
            if (HubUserData.connectedList.ContainsKey(data.UserName))
            {
                logger.LogInformation($"User connected again (reconnected): {userName}");
            }
            else
            {
                HubUserData.connectedList.Add(data.UserName, data);
            }

            logger.LogInformation($"User connected: {userName}");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = this.Context.User.Identity.Name;
            var data = new HubUserData { UserName = userName, Status = 0, UpdatedOn = DateTime.Now };
            HubUserData.list.Add(data);
            HubUserData.connectedList.Remove(data.UserName);
            logger.LogInformation($"User disconnected: {userName}");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
