using Microsoft.AspNetCore.SignalR;
using WebSignalRChat.Services;

namespace WebSignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;

        public ChatHub(ChatService chatService) 
        {
            _chatService=chatService;
        }

        //public async Task Send(string message, string userName)
        //{
        //    await Clients.All.SendAsync("Send", message, userName);
        //}


        public override async Task OnConnectedAsync()
        {
            //await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
            _chatService.Connections.Add(Context.ConnectionId);

            await Clients.All.SendAsync("UpdateOnline", _chatService.Connections);

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
            var connection = _chatService.Connections.FirstOrDefault(x => x == Context.ConnectionId);

            if (connection != null) _chatService.Connections.Remove(connection);

            await Clients.All.SendAsync("UpdateOnline", _chatService.Connections);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task CreateMessage(string message, string? connectionId)
        {
            if (connectionId == null)
            {
                await Clients.All.SendAsync("SendMessage", message);
            }
            else 
            {
                await Clients.Caller.SendAsync("SendMessage", message);
                await Clients.Client(connectionId).SendAsync("SendMessage", message);
            }
        }

        public async Task Invite(string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("SendInvite", Context.ConnectionId);
        }

        public async Task Out(string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("SendInvite", Context.ConnectionId);
        }

    }
}
