using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SignarRWebAPIDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Learning Reference - 
/// To detect multiple connecction - https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/mapping-users-to-connections
/// </summary>
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
            var userIdentifier = this.Context.UserIdentifier;

            if (HubUserData._connections.GetConnections(userName.ToUpper()).Count()>0)
            {
                await OnMultipleConnectionAttempted(userIdentifier);
            }
            else
            {
                HubUserData._connections.Add(userName.ToUpper(), Context.ConnectionId);
            }
            logger.LogInformation($"User connected: {userName} with userIdentifier is {userIdentifier}");
            await SendOnlineUsers();
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = this.Context.User.Identity.Name;
 
            HubUserData.connectedList.Remove(userName.ToUpper());
            logger.LogInformation($"User disconnected: {userName}");


            HubUserData._connections.Remove(userName.ToUpper(), Context.ConnectionId);
            
            await SendOnlineUsers();
            await base.OnDisconnectedAsync(exception);
        }
       
        public Task OnMultipleConnectionAttempted(string userId)
        {
           
            return Clients.Caller.SendAsync("onMultipleConnectionAttempted", $"Already logged in with userid: {userId}");

        }
        public Task SendOnlineUsers()
        {
            var l = HubUserData._connections.GetKeys().ToList();
           
            HubUserData.connectedList.Clear();
            foreach(var item in l)
            {
                HubUserData.connectedList.Add(item, new HubUserData() { UserName = item, Status = 1, UpdatedOn = DateTime.Now });
            }
            var list = HubUserData.connectedList.Select(x => x.Value);
            return Clients.All.SendAsync("getOnlineUsers", list);
        }
    }
}
