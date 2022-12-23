using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using WebSignalRChat.Models;
using WebSignalRChat.Services;

namespace WebSignalRChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        private readonly ChatService _chatService;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context, ChatService chatService)
        {
            _logger = logger;
            _context = context;
            _chatService = chatService;
        }

        public IActionResult Index(SendModel SendModel)
        {
            RecurringJob.AddOrUpdate(() => DeleteSend(), Cron.Minutely);
            return View(_context.SendModels);
        }

        [HttpDelete]

        public IActionResult DeleteSend() 
        {
            //SendModel SendModel = _context.SendModels.FirstOrDefault();

            //if (SendModel == null)
            //    return NotFound();

            
            _context.SendModels.RemoveRange(_context.SendModels);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DataInputAdd(SendModel SendModel)
        {
            _context.SendModels.Add(SendModel);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IActionResult Spisok()
        {
            return View(_context.SendModels);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}