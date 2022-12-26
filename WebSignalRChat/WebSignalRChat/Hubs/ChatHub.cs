using Microsoft.AspNetCore.SignalR;
using WebSignalRChat.Models;
using WebSignalRChat.Services;
using Hangfire;
using AutoMapper;

namespace WebSignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public object Id { get; private set; }

        public ChatHub(ChatService chatService, ApplicationContext context, IMapper mapper)
        {
            _chatService = chatService;
            _context = context;
            _mapper = mapper;
        }

        public async Task runprocess()
        {

            _context.SendModels.RemoveRange(_context.SendModels);
            _context.SaveChanges();
        }

        public override async Task OnConnectedAsync()
        {
            //await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
            _chatService.Connections.Add(Context.ConnectionId);

            RecurringJob.AddOrUpdate(() => runprocess(), "* * * * *");

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
            var SendModel = new SendModel { From= Context.ConnectionId, Value= message, TimeOnly = DateTime.Now};
            _context.SendModels.Add(SendModel);
            _context.SaveChanges();

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

    }
}
