using Microsoft.EntityFrameworkCore;

namespace WebSignalRChat.Services
{
    public class ChatService
    {
        public List<string> Connections { get; set; } = new List<string>();

        public async Task runprocess()
        {

            _context.SendModels.RemoveRange(_context.SendModels);
            _context.SaveChanges();
        }

    }
}
