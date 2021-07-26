using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // 각종 서비스를 추가 (DI) 영업 시작!
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // DI 서비스란? SRP(Single Responsibility Principle)
            // ex) 랭킹 관련 기능이 필요하면 -> 랭킹 서비스 만들어서 필요한걸 가져다 씀
            //     모든걸 서비스로 하나씩 분리해서 관리한다는게 DI의 기본적인 개념
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // HTTP Reqeust Pipeline (NodeJS와 유사)
        // 어떤 HTTP 요청이 왔을 때 앱이 어떻게 응답하는지 일련의 과정을 나타냄
        // 1) IIS, Apache 등에 HTTP 요청 
        // 2) ASP.NET Core 서버 (Kestrel) 전달 
        // 3) 미들웨어 적용
        //      미들웨어 : HTTP request/response 를 처리하는 중간 부품 
        // 4) Controller로 전달 (파이프라인 적용)
        // 5) Controller에서 처리하고 View로 전달 

        // [Request]                 [Response]
        //     [파이프라인]      [파이프라인]
        //         [마지막 MVC EndPoint]

        // 미들웨어에서 처리한 결과물을 다른 미들웨어로 넘길 수 있다 
        // [파이프라인]

        // [!] Controller에서 처리하지 않는 이유?
        // ex) 모든 요청마다 로깅을 해야 한다면?
        //  모든 코드에 코드를 넣는건 코드가 많아지고 반복적인 힘든 작업. ?
        //  미들웨어가 모든 요청에 대해 일괄적으로 처리해줌 

        // 어떤 미들웨어에서 에러가 발생하면 
        // 다시 위로 쭉 에러를 전파시킴

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseStatusCodePages();
            //app.UseStaticFiles();  // root 폴더에 있는 파일을 전달해주는 간단한 기능
            //app.UseWelcomePage();   // 순서가 중요할 수 있음. UseWelcomePage 부터 실행하면 중간에 요청을 먹어버리고 다시 돌려보냄

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // CSS, JavaScript, 이미지 등 요청 받을 때 처리   
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // 라우팅(Routing)
            // 라우팅은 길잡이 라는 의미
            // 라우팅이 하는 역할 : HTTP request가 왔을 때 담당 Handler에 넘겨주기 위해서
            //                    [HTTP request] <-> [담당 Handler] 둘 사이를 Mapping함

            // APT.NET 초기 버전에서는 /hello.aspx와 같이 처리하는 파일 자체를 URL에 입력 
            // 단점
            //   1) 파일 이름이 바뀌면? 클라이어트 쪽에서 같이 처리하지 않으면 접속 불가능
            //   2) /hello.aspx?method=1&id=3 .. 와 같이 QueryString 방식의 URL
            //       -> 지금 방식은 /hello/get/3 과 같이 뜻을 명확히 알 수 있게 사용

            // 기본 관례(Convention)는 Controller/Action/Id 형식
            // 다른 이름 지정하고 싶을떈?
            //  - API 서버로 사용하고 싶을 때, URL 주소가 어떤 역할을 하는지 더 명확하게 힌트를 주고 싶다거나
            //  - 굳이 Controller를 수정하지 않고 연결된 URL만 교체하고 싶다!

            // Routing이 적용되려면 [미들웨어 파이프라인]에 의해 전달이 되어야 함
            // - 중간에 에러가 난다거나, 특정 미들웨어가 흐름을 가로챘다면 라우팅 X

            // 파이프라인 끝까지 도달했으면, MapControllerRoute에 의해 Routing 규칙이 결정
            //                                              (원하는 패턴 여러개를 설정 가능)
            // - 패턴을 이용한 방식으로 Routing이 적용되서 주소를 찾지만
            // - 경우에 따라 Attribute Routing이란걸 사용해서 Routing 규칙을 덮어쓸 수 있음 

            // Route Template (Pattern) - 아래 보이는 라우팅 패턴 매칭
            // name : "default" -> 다수를 설정 할 수 있다는 의미

            app.UseEndpoints(endpoints =>
            {
                // api : literal value ( 고정 문자열 값? 꼭 필요함)
                // {controller}, {action} : route parameter (꼭 필요함)
                // {controller=Home}/{action=Index} : Optional Route parameter (없으면 알아서 기본값 설정)
                // {id?} : Optional Route parameter (없어도 됨)
                // [주의!] {controller}, {action} 은 무조건 정해져야 한다! (매칭 or 기본값을 통해서)
                // 라우팅 패턴 순서를 경우에 따라 주의해서 정해줘야 한다.

                // Constraint(제약사항) 관련
                // {controller=Home}/{action=Index}/{id?}
                // 예를들어 id의 형식이 광범위하다는 문제가 있음 (int여야 하는데 string이 온다거나)
                //    {cc:int} 정수만
                //    {cc:min(18)} 18이상 정수만
                //    {cc:length(5)} 5글자 string

                // Default Value와 Constraint를 설정하는 2번째 방법 (Anonymous Object)

                // Match-All (조커카드)
                // {*jocker} *를 붙이면 모든 문자열을 다 매칭시켜줌 ('/' 도 포함해서)
                // 가장 광범위하므로 왠만하면 맨 마지막에 둬야한다

                // Redirection : 다른 URL로 토스
                // Redirection(URL) << URL 직접 만들어서
                // - Url.Action
                // - Url.RouteUrl
                // RedirectToAction()
                // RedirectToRoute()

                //endpoints.MapControllerRoute(
                //    name: "test",
                //    pattern: "api/{controller}/{action}/{test:int?}",
                //    defaults: new {controller="Home", action="Privacy"} );

                // 아래와 같이 설정도 가능
                endpoints.MapControllerRoute(
                  name: "test",
                  pattern: "api/{test}",
                  defaults: new { controller = "Home", action = "Privacy" },
                  constraints: new { test = new IntRouteConstraint() });

                // 라우팅 패턴 설정
                endpoints.MapControllerRoute(
                    name: "default", 
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "jocker",
                    pattern: "{*jocker}",
                    defaults: new { controller = "Home", action = "Error" });


            });

        }
    }
}
