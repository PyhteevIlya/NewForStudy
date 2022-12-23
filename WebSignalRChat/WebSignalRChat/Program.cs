using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Storage.SQLite;
using Microsoft.EntityFrameworkCore;
using WebSignalRChat;
using WebSignalRChat.Hubs;
using WebSignalRChat.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddSingleton<ChatService>();

builder.Services.AddHangfire(config =>config
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSQLiteStorage(builder.Configuration.GetConnectionString("ChatDBConnection"))
);

var connectionString = builder.Configuration.GetConnectionString("ChatDBConnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
options.UseSqlite(connectionString));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chat");

app.Run();
