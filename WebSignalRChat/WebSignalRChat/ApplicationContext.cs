using Microsoft.EntityFrameworkCore;
using WebSignalRChat.Models;

namespace WebSignalRChat
{
    public class ApplicationContext : DbContext
    {
        public DbSet<SendModel> SendModels { get; set; }
        public ApplicationContext() => Database.EnsureCreated();

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
