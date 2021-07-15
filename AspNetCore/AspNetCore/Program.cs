using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore
{
    // 근데 왜 Program과 Startup으로 셋팅을 두 번에 나눠서 할까?
    // Program Class에서는 약간 거시적인 관점에서의 설정 (HTTP 서버, IIS 사용 여부 등.
    //                                                 한번 설정해주면 거의 바뀌지 않음)
    // Startup은 세부적인 설정 ( 미들웨어 설정,
    //                  Dependency Injection(새로운 서비스를 시작한다는 느낌으로 이해하면 됨)
    //                  등 필요에 따라 추가/조립)


    public class Program
    {
        // 일반적인 콘솔 앱
        public static void Main(string[] args)
        {
            // 3) IHost를 만듬
            // 4) 구동(Run)  <  이떄부터 Listen 을 시작
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        // 1) 각종 옵션 설정을 셋팅
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //2) Startup 클래스 지정
                    webBuilder.UseStartup<Startup>();
                });
        
        // 아래와 같은 코드임
        //public static IHostBuilder CreateHostBuilder(string[] args)
        //    {
        //        return Host.CreateDefaultBuilder(args)
        //            .ConfigureWebHostDefaults(...)
        //              ...
        //    }
                    
    }
}
