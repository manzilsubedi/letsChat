using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsChat.Hubs
{
    [Authorize]
    public class Chathub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
