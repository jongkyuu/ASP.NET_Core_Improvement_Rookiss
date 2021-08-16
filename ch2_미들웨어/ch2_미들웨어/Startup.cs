using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch2_middleWare
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


        // HTTP Request Pipeline (NodeJS와 유사)
        // 미들웨어 : HTTP request / response를 처리하는 중간 부품
        // [Request]                 [Response]
        //     [파이프라인]      [파이프라인]
        //         [마지막 MVC EndPoint]

        // 미들웨어에서 처리한 결과물을 다른 미들웨어로 넘길 수 있다
        // [!] Controller에서 처리하지 않는 이유는?
        //   -> 모든 요청마다 로깅을 해야 하는 경우를 예로 들면
        //      Controller의 모든 코드(메소드)에다가 로깅 기능을 일일이 넣는건 비효율적이고 관리도 힘듬
        //      이런 식이면 새로운 기능을 추가할때마다 반복적인 작업을 해야함
        //      일괄적으로 모든 요청에 대해 처리할때 사용하는게 미들웨어

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.UseStatusCodePages(); // 존재하지 않는 페이지에 접근하면 "Status Code: 404; Not Found" 메세지 출력
            
            app.UseExceptionHandler("/Home/Error");  // 맨 밑단에서 Response 쪽으로 타고 올라가다가
                                                     // Response쪽 파이프라인에서 Error 발생하면
                                                     // 마치 "/Home/Error"로 Request가 온것처럼
                                                     // Request쪽 파이프라인에서 다시 타고 내려온다
                                                     // 응답과 요청 사이가 이런식으로 복잡하게 얽힐 수도 있음
            
            // 모든 부품들이 다 필요하지 않다는걸 보여주기 위한 실습
            //app.UseStaticFiles();
            //app.UseWelcomePage(); // 중간 부품이라고 해도 순서가 중요할 수 있다
            //                      // app.UseWelcomePage() 부터 배치하면 아래로 요청을 내려보내지 않고 
            //                      // 요청을 다 먹어버린 후 (커팅?) 다시 위로 올려보냄(Response쪽으로)

            //if (env.IsDevelopment())  // 개발 상태냐 아니냐에 따라 
            //{
            //    app.UseDeveloperExceptionPage();  // DeveloperExceptionPage 로 상세 원인을 보여줌
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");  // 그게 아니면 /Home/Error 에 보냄
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();

            // CSS, JavaScript, 이미지 등 요청 받을 때 처리   
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // 라우팅 패턴 설정
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
