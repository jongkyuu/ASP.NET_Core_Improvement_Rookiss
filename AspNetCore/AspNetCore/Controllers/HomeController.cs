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
    // MVC ( Model - View - Controller)
    // Django, Rails, Spring, ASP.Net 등에 모두 있는 설계 패턴
    // 
    // Model : 데이터 모델을 의미, 메모리 파일 DB등을 이용해 정보 추출 (식당에서 재료 준비)
    // Controller : 데이터 가공, 필터링, 유효성 체크, 서비스 호출 (재료를 가공하는 단계)
    // + 각종 서비스 : 서비스를 이용해 요리. DI로 ConfigureServices에서 추가하는 서비스를 의미
    // View : 최종 결과물을 어떻게 보여줄지 (최종 서빙)
    // 이런 식으로 역할 분담을 나누는 것이 MVC의 철학임

    // 이렇게 역할 분담 할때의 장점은?
    // 유동적으로 기능을 변경 할 수 있다. 
    // ex) SPA(Json) Web(Html) 결과물이 다르면 View Layer만 바꾸고, Controller 재사용 가능
    // 
    // Action은 요청에 대한 실제 처리 함수(Handler)
    // Controller는 Action을 포함 하고 있는 그룹 

    // Contoller 상속이 무조건 필요한 것은 아님 
    // View() 처럼 이미 정의된 Helper 기능 사용하고 싶으면 필요 
    // UI(View)와 관련된 기능을 뺀 ControllerBase -> WebApi

    // MVC에서 Controller의 역할은 데이터 가공, UI랑은 무관하다 (실질적인 기능 수행은 별도의 서비스를 이용)
    // 넘길 때 IActionResult 넘긴다 ( View를 넘기는데 상속으로 구성되어 있기 때문)
    // 자주 사용되는 IActionResult 종류
    // 1) ViewResult : HTML View를 생성
    // 2) RedirectResult : 요청을 다른 곳으로 토스 (다른 페이지로 연결해 줄 때 사용)
    // 3) FileResult : 파일을 반환
    // 4) ContentResult : 특정 string 반환
    // 5) StatusCodeResult : Http status code 반환
    // 6) NotFoundResult : 404 HTTP status code 반환 (404 못찾음!)

    // new ViewResult() -> View()   둘다 똑같은 의미.  View()는 헬퍼 함수. 헬퍼 함수를 사용하기 위해서는 Controller를 상속받아야함
    // new RedirectResult() -> Redirect()
    // new NotFoundResult() -> NotFound()

    // 결론 : MVC를 사용하면 역할 분담이 확실해진다
    // 서로 종속된 코드를 만들면 하나를 수정할 때 다른 쪽에서 문제가 발생하는 일이 빈번하다
    // MVC는 역할 분담으로 인해 코드의 종속성을 제거함
    // MVC에서 V를 빼면 [MC]만 사용하게 될 경우 Web API가 됨
    // 결과적으로 MVC나 WebAPI나 설계 철학 제체는 큰 차이가 없다

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //string url = Url.Action("Privacy", "Home");
            string url = Url.RouteUrl("test", new { test = 123 });
            return Redirect(url);
            //return View();
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
