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


            app.UseExceptionHandler("/Home/Error");
            
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
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
