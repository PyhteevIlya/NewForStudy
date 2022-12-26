using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context, ChatService chatService, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _chatService = chatService;
            _mapper = mapper;
        }

        public IActionResult Index(SendModel SendModel)
        {
            var sendModel = _context.SendModels.ToList();
            var mapSendModel = _mapper.Map<List<MapSendModel>>(sendModel);

            //var sendModel = _context.SendModels.FirstOrDefault();

            //var mapSendModel = _mapper.Map<MapSendModel>(sendModel);


            return View(mapSendModel);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}