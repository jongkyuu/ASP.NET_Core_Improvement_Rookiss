using ch1_MVC_복습.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ch1_MVC_복습.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 서버주소/Home/Privacy 주소로 접근
        public IActionResult Privacy()
        {
            // Controller에서 View로 데이터를 넘기는 방법
            ViewData["Message"] = "Data From Privacy";
            // ViewData는 선언해 준 적이 없지만 Controller 안에 구현되어 있음 

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
