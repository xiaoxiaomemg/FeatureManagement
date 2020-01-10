using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FeatureManagerTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// 安装包 Microsoft.Azure.AppConfiguration.AspNetCore
        /// 通过配置将APPconfig 和程序进行连接
        /// 好处：将feature flags 全都保留在程序外部 并分别进行管理；可随时修改feature flags状态，且状态在程序中能立即生效
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreatWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostringContext, config) =>
        {
            var setting = config.Build();
            config.AddAzureAppConfiguration(options =>
            {
                options.Connect(setting["ConnectionStrings:AppConfig"]).UseFeatureFlags();
            });
        }).UseStartup<Startup>();
    }
}