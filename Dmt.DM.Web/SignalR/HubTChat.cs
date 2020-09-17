using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Dmt.DM.Web.SignalR
{
    public class HubTChat : Hub<IChatClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.Send($"{Context.ConnectionId} joined");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.Others.Send($"{Context.ConnectionId} left");
        }

        public Task Send(string message)
        {
            return Clients.All.Send($"{Context.ConnectionId}: {message}");
        }

        public Task AddNotify(NotifyModel notify)
        {
            return Clients.All.AddNotify(notify);
        }

        public Task SendToOthers(string message)
        {
            return Clients.Others.Send($"{Context.ConnectionId}: {message}");
        }

        public Task SendToGroup(string groupName, string message)
        {
            return Clients.Group(groupName).Send($"{Context.ConnectionId}@{groupName}: {message}");
        }

        public Task AddNotifyToGroup(string groupName, NotifyModel notify)
        {
            return Clients.Group(groupName).AddNotify(notify);
        }

        public Task SendToOthersInGroup(string groupName, string message)
        {
            return Clients.OthersInGroup(groupName).Send($"{Context.ConnectionId}@{groupName}: {message}");
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).Send($"{Context.ConnectionId} joined {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Clients.Group(groupName).Send($"{Context.ConnectionId} left {groupName}");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public Task Echo(string message)
        {
            return Clients.Caller.Send($"{Context.ConnectionId}: {message}");
        }
    }

    public interface IChatClient
    {
        Task Send(string message);
        Task AddNotify(NotifyModel notify);
    }
}
