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
    // MVC (Model-View-Controller)
    // Django, Rails, Spring 등에 다 존재하는 개념

    // Model ( 메모리, 파일, DB등에서 정보 추출 ), 요리로 치면 재료를 준비하는 단계
    // Controller ( 데이터 가공, 필터링, 유효성 체크, 서비스 호출 ), 재료를 꺼내서 가공하는 단계
    // 요리는 누가? 
    // + 각종 서비스 -> 요리 담당
    // View ( 최종 결과물을 어떻게 보여줄지 ), 최종 서빙

    // 이렇게 역할을 분담할 때 장점은?
    // 유동적으로 기능을 변경 할 수 있다. 
    // ex) SPA(Json), Web(HTML) 결과물이 다르면 View Layer만 바꾸고, Controller는 재사용 가능

    // Action은 요청에 대한 실제 처리 함수 (Handler)
    // Controller는 Action을 포함하고 있는 그룹

    // Controller 상속이 무조건 필요한 것은 아님
    // View()처럼 이미 정의된 Helper 기능을 사용하고 싶으면 필요
    // UI(View)와 관련된 기능을 뺀 ControllerBase도 있음 -> WebAPI 프로젝트를 만들면 HomeController가 ControllerBase를 상속

    // MVC에서 Controller는 각종 데이터 가공을 담당하고 UI와는 무관하다
    // 넘길 때 IActionResult로 넘긴다.
    // 자주 사용되는 IActionResult 종류
    // 1) ViewResult : HTML View 생성
    // 2) RedirectResult : 요청을 다른 곳으로 토스 ( 다른 페이지로 연결해줄 때 사용. ex) 요청을 결제 페이지로 넘긴다거나)
    // 3) FileResult : 파일을 반환 
    // 4) ContentResult : 특정 string을 반환
    // 5) StatusCodeResult : HTTP Status Code를 반환 
    // 6) NotFoundResult : 404 HTTP Status Code를 반환 ( 404가 못찾았음을 의미 )

    // View()는 Helper 클래스
    // return new ViewResult();  이런식으로 반환해도 됨
    // new를 매번 하기 귀찮아서(?) Helper 클래스가 있고
    // View()도 ViewResult를 반환. 타고 들어가면 public virtual ViewResult View();
    // new ViewResult() --> View() 와 같음
    // View() 같은 헬퍼 함수를 사용하기 위해서는 Controller를 상속받아야 함

    // 나머지도 Helper 함수가 있음
    // new ViewResult() --> View()
    // new RedirectResult --> Redirect()
    // new NotFoundResult() --> NotFound()

    // 결론 : MVC를 사용하면 역할 분담이 확실해진다
    // 서로 종속된 코드를 만들면, 하나를 수정할 때 다른 쪽에서 문제가 터지는 일이 빈번
    // MVC는 역할 분담으로 인해 코드 종속성을 제거
    // MVC에서 V를 빼면 결국 WebAPI가 된다
    // 결과적으로 MVC나 WebAPI나 설계 철학 자체에 큰 차이는 없다 

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
