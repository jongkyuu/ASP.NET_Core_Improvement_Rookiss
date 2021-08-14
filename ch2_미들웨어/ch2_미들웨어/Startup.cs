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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())  // 개발 상태냐 아니냐에 따라 
            {
                app.UseDeveloperExceptionPage();  // DeveloperExceptionPage 로 상세 원인을 보여줌
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");  // 그게 아니면 /Home/Error 에 보냄
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
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
