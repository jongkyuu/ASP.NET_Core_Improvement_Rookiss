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

            // CSS, JavaScript, �̹��� �� ��û ���� �� ó��   
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // �����(Routing)
            // ������� ������ ��� �ǹ�
            // ������� �ϴ� ���� : HTTP request�� ���� �� ��� Handler�� �Ѱ��ֱ� ���ؼ�
            //                    [HTTP request] <-> [��� Handler] �� ���̸� Mapping��

            // APT.NET �ʱ� ���������� /hello.aspx�� ���� ó���ϴ� ���� ��ü�� URL�� �Է� 
            // ����
            //   1) ���� �̸��� �ٲ��? Ŭ���̾�Ʈ �ʿ��� ���� ó������ ������ ���� �Ұ���
            //   2) /hello.aspx?method=1&id=3 .. �� ���� QueryString ����� URL
            //       -> ���� ����� /hello/get/3 �� ���� ���� ��Ȯ�� �� �� �ְ� ���

            // �⺻ ����(Convention)�� Controller/Action/Id ����
            // �ٸ� �̸� �����ϰ� ������?
            //  - API ������ ����ϰ� ���� ��, URL �ּҰ� � ������ �ϴ��� �� ��Ȯ�ϰ� ��Ʈ�� �ְ� �ʹٰų�
            //  - ���� Controller�� �������� �ʰ� ����� URL�� ��ü�ϰ� �ʹ�!

            // Routing�� ����Ƿ��� [�̵���� ����������]�� ���� ������ �Ǿ�� ��
            // - �߰��� ������ ���ٰų�, Ư�� �̵��� �帧�� ����ë�ٸ� ����� X

            // ���������� ������ ����������, MapControllerRoute�� ���� Routing ��Ģ�� ����
            //                                              (���ϴ� ���� �������� ���� ����)
            // - ������ �̿��� ������� Routing�� ����Ǽ� �ּҸ� ã����
            // - ��쿡 ���� Attribute Routing�̶��� ����ؼ� Routing ��Ģ�� ��� �� ���� 

            // Route Template (Pattern) - �Ʒ� ���̴� ����� ���� ��Ī
            // name : "default" -> �ټ��� ���� �� �� �ִٴ� �ǹ�

            app.UseEndpoints(endpoints =>
            {
                // api : literal value ( ���� ���ڿ� ��? �� �ʿ���)
                // {controller}, {action} : route parameter (�� �ʿ���)
                // {controller=Home}/{action=Index} : Optional Route parameter (������ �˾Ƽ� �⺻�� ����)
                // {id?} : Optional Route parameter (��� ��)
                // [����!] {controller}, {action} �� ������ �������� �Ѵ�! (��Ī or �⺻���� ���ؼ�)
                // ����� ���� ������ ��쿡 ���� �����ؼ� ������� �Ѵ�.

                // Constraint(�������) ����
                // {controller=Home}/{action=Index}/{id?}
                // ������� id�� ������ �������ϴٴ� ������ ���� (int���� �ϴµ� string�� �´ٰų�)
                //    {cc:int} ������
                //    {cc:min(18)} 18�̻� ������
                //    {cc:length(5)} 5���� string

                // Default Value�� Constraint�� �����ϴ� 2��° ��� (Anonymous Object)

                // Match-All (��Ŀī��)
                // {*jocker} *�� ���̸� ��� ���ڿ��� �� ��Ī������ ('/' �� �����ؼ�)
                // ���� �������ϹǷ� �ظ��ϸ� �� �������� �־��Ѵ�

                // Redirection : �ٸ� URL�� �佺
                // Redirection(URL) << URL ���� ����
                // - Url.Action
                // - Url.RouteUrl
                // RedirectToAction()
                // RedirectToRoute()

                //endpoints.MapControllerRoute(
                //    name: "test",
                //    pattern: "api/{controller}/{action}/{test:int?}",
                //    defaults: new {controller="Home", action="Privacy"} );

                // �Ʒ��� ���� ������ ����
                endpoints.MapControllerRoute(
                  name: "test",
                  pattern: "api/{test}",
                  defaults: new { controller = "Home", action = "Privacy" },
                  constraints: new { test = new IntRouteConstraint() });

                // ����� ���� ����
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
