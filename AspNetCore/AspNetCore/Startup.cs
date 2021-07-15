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
        // ���� ���񽺸� �߰� (DI) ���� ����!
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // DI ���񽺶�? SRP(Single Responsibility Principle)
            // ex) ��ŷ ���� ����� �ʿ��ϸ� -> ��ŷ ���� ���� �ʿ��Ѱ� ������ ��
            //     ���� ���񽺷� �ϳ��� �и��ؼ� �����Ѵٴ°� DI�� �⺻���� ����
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // HTTP Reqeust Pipeline (NodeJS�� ����)
        // � HTTP ��û�� ���� �� ���� ��� �����ϴ��� �Ϸ��� ������ ��Ÿ��
        // 1) IIS, Apache � HTTP ��û 
        // 2) ASP.NET Core ���� (Kestrel) ���� 
        // 3) �̵���� ����
        //      �̵���� : HTTP request/response �� ó���ϴ� �߰� ��ǰ 
        // 4) Controller�� ���� (���������� ����)
        // 5) Controller���� ó���ϰ� View�� ���� 

        // [Request]                 [Response]
        //     [����������]      [����������]
        //         [������ MVC EndPoint]

        // �̵����� ó���� ������� �ٸ� �̵����� �ѱ� �� �ִ� 
        // [����������]

        // [!] Controller���� ó������ �ʴ� ����?
        // ex) ��� ��û���� �α��� �ؾ� �Ѵٸ�?
        //  ��� �ڵ忡 �ڵ带 �ִ°� �ڵ尡 �������� �ݺ����� ���� �۾�. ?
        //  �̵��� ��� ��û�� ���� �ϰ������� ó������ 

        // � �̵����� ������ �߻��ϸ� 
        // �ٽ� ���� �� ������ ���Ľ�Ŵ

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseStatusCodePages();
            //app.UseStaticFiles();  // root ������ �ִ� ������ �������ִ� ������ ���
            //app.UseWelcomePage();   // ������ �߿��� �� ����. UseWelcomePage ���� �����ϸ� �߰��� ��û�� �Ծ������ �ٽ� ��������


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

            // CSS, JavaScript, �̹��� �� ��û ���� �� ó��   
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // ����� ���� ����
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
