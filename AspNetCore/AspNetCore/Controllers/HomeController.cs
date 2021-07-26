using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Controllers
{
    // 테스트입니다
    // 일반적인 MVC 순서
    // 1) 클라에서 HTTP Request가 옴
    // 2) Routing에 의해 Controller / Action 정해짐
    // 3) Model Biinding으로 Request에 있는 데이터를 파싱(Validation)
    // 4) 담당 서비스로 전달 (Application Model로 바꿔서 전달)
    // 5) 담당 서비스가 결과물을 Action에 돌려주면
    // 6) Aciton에서 ViewModel을 이용해서 View로 전달
    // 7) View에서 HTML 생성
    // 8) Response로 HTML 결과물을 전송

    // V (View)
    // Razor View Page (.cshtml) 
    // cshtml은 기본적으로 HTML과 유사함
    // HTML은 동적 처리가 애매하다
    //  - 동적이라 함은 if, else 분기문 처리라거나,
    //  - 특정 리스트 개수에 따라서 <ul><li>
    // 따라서 C#을 이용해서 생명을 불어넣겠다!
    // HTML은 고정적인 부분을 담당(실제로 클라에 응답을 줄 HTML)
    // C#은 동적으로 변하는 부분을 담당

    // index.cshtml 같은 파일 자체가 Razor Tmeplate 
    // Razor Tmeplate을 우리가 만들어 주고
    // 이를 Razor Template Engine이 Template를 분석해서 최종 결과물을 동적으로 생성

    // 일반적으로 View에 대이터를 건내주는 역할은 Action에서 한다


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // 기본적으로 Views/Controller/Action.cshtml을 템플릿으로 사용
        // 이 Action이라면 Views/Home/Test.cshtml
        public IActionResult Test()
        {
            return View(); // 헬퍼함수 이용한것임. View -> new ViewResult

            // 상대 경로
            // return View("Privacy");

            // 절대 경로 - 최상위폴더인 Views와 .cshtml을 다 추가해줘야함
            // return View("Views/Shared/Error.cshtml");
        }

        public IActionResult Index()
        {
            string url = Url.RouteUrl("test", new { test = 123 });

            return Redirect(url);
            //return View();
            //return RedirectToAction("Privacy");
        }

        public IActionResult Privacy()
        {
            // 데이터 넘기는 테스트
            ViewData["Message"] = "Data From Privacy";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
