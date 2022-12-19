using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSignalRChat.Models;

namespace WebSignalRChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DataInputAdd(SendModel DataInput)
        {
            _context.SendModels.Add(DataInput);
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