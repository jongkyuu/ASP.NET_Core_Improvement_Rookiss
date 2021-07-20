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
    // M(Model) 
    // 데이터 모델
    // - Binding Model
    // 클라에서 보낸 Request를 파싱하기 위한 데이터 모델 << 유효성 검증 필수(클라는 신용할 수 없음)
    // - Application Model
    // 서버의 각종 서비스들이 사용하는 데이터 모델 ( ex. RankingService라면 RankingData)
    // - View Model
    // Response UI를 만들기 위한 데이터 모델
    // - API Model
    // WebAPI Controller에서 JSON / XML 포맷으로 응답할 때 필요한 데이터 모델

    // 일반적인 MVC 순서
    // 1) 클라에서 HTTP Request가 옴
    // 2) Routing에 의해 Controller / Action 정해짐
    // 3) Model Biinding으로 Request에 있는 데이터를 파싱(Validation)
    // 4) 담당 서비스로 전달 (Application Model로 바꿔서 전달)
    // 5) 담당 서비스가 결과물을 Action에 돌려주면
    // 6) Aciton에서 ViewModel을 이용해서 View로 전달
    // 7) View에서 HTML 생성
    // 8) Response로 HTML 결과물을 전송

    // Model Binding
    // 1) Form Values
    //    - Request의 Body에서 보낸 값(HTTP Post 방식의 요청)
    // 2) Routes Values
    //    - URL 매칭, Defalut Values
    // 3) Query String Values
    //    - URL끝에 데이터를 붙이는 방법, ?Name=Rookiss (HTTP GET 방식의 요청)

    // 매우 감동적 -> 일일히 추출하지 않고 마치 ORM을 사용하듯이 편리하게! 알아서! 파라미터를 채워서 함수를 호출한다는게 신기
    // 다른 프레임워크에서는 수동으로 하나하나 꺼내야 하는 경우가 있음

    // Route + Query String 혼합해서 사용 가능하지만, 
    // 하나만 골라서 사용하는게 일반적임 -> 굳이 혼합한다면 ID만 Route 방식으로 넘기는 식으로 응용

    // Complex Type
    // 넘겨받을 인자가 너무 많아지면 부담스러우니까
    // 그냥 별도의 모델링 클래스를 만들어주면 된다 (Test2 메서드 참고)

    // Collections
    // 더 나아가서 List나 Dictionary로도 매핑을 할 수가 있음!

    // Binding Source 지정
    // 기본적으로 Binding Model은 Form, Route, QueryString
    // 위의 삼총사 중 하나를 명시적으로 지정해서 파싱하거나, 다른 애로 지정할 수도 있음
    // ex) 대표적으로 Body에서 JSON 형태로 데이터를 보내주고 싶을 떄
    // 참고) Part9 WebAPI 실습에서도 사용한 적이 있음
    // [FromHeader] HeaderValue에서 찾아라
    // [FromQuery] QueryString에서 찾아라
    // [FromRoute] Route Parameter에서 찾아라
    // [FromForm] POST Body에서 찾아라
    // [FromBody] 그냥 Body에서 찾아라 (디폴트 JSON -> 다른 형태로도 셋팅 가능)


    // Validation
    // 클라이언트-서버 구조는 늘 그렇지만, 신용할 수 없음
    // 전화번호를 입력하라고 했는데 abcd 문자열을 보낸다거나
    // 사진을 보내라고 했는데 10GB 파일을 보낸다거나
    // 구매 수량을 음수로 보냈다거나 하는 등..

    // Q) 근데 클라에서 Javascript 등으로 체크를 해서 걸러내면 되지 않을까?
    // A) 정상적인 유저라면 OK. 그러나 게임서버나 웹에서 클라는 잠재적인 범죄자!


    // DataAnnotation
    // 데이터에 힌트를 줘서 강압적으로 체크를 하겠다는 의미
    // Blazor 등에서 UI를 만들때도 사용
    // 공용으로 사용되는 모델의
    //   장점 : 기본 검증모델을 하나만 만들고 그걸 UI, 서버 등에서 사용할 수 있음
    //   단점 : 세부적인 검사는 하기 힘들다

    // [Required] 무조건 있어야 함
    // [CreaditCard] 올바른 결제카드 번호인지
    // [EmailAddress] 올바른 이메일 주소인지
    // [StringLength(max)] String 길이가 최대 max인지
    // [MinLength(min)] Collection의 크기가 최소 min인지
    // [Phone] 올바른 전화번호인지
    // [Range(min, max)] Min-Max 사이의 값인지
    // [Url] 올바른 URL인지
    // [Compare] 2개의 Property 비교 ex) (Password, ConfirmPassword)

    // [!] Validation은 Attribute만 적용하면 알아서 자동으로 적용되지만,
    // 하지만 결과에 대해서 어떻게 처리할지는 Action에서 정해야함
    // Validation 결과를 ContollerBase의 ModelState에 저장한다

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Test(int id, [FromHeader] string value)
        {
            return null;
        }
        public IActionResult Test2(TestModel testModel)
        {
            if (!ModelState.IsValid)   // Validation 실패시 ModelState.IsValid == True
                return RedirectToAction("Error");
            return null;
        }
        public IActionResult Test3(List<string> names)
        {
            return null;
        }

        public IActionResult Index()
        {
            //string url = Url.Action("Privacy", "Home");
            //string url = Url.RouteUrl("test", new { test = 123 });

            //return Redirect(url);
            //return View();
            return RedirectToAction("Privacy");
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
