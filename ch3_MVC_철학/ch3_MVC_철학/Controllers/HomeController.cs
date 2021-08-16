using AspDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNetCore.Controllers
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
            // 에러 나는 상황 만들기(0으로 나눠준 case)
            int a = 1;
            int b = 3 / (a - 1);  // 여기서 에러 발생. 
                                  // EndPoint에서 에러가 위로 전파되면 
                                  // app.UseExeptionHandler("/Home/Error")가 흐름을 가로채서
                                  // Request쪽 파이프라인으로 가서 "/Home/Error"에 대한 request가 온 것처럼 요청을 하고 
                                  // 다시 EndPoint에 가서 라우팅 규칙에 따라 
                                  // HomeController의 Error() 호출



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
