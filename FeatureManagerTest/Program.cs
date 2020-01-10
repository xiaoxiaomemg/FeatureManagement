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
        /// ��װ�� Microsoft.Azure.AppConfiguration.AspNetCore
        /// ͨ�����ý�APPconfig �ͳ����������
        /// �ô�����feature flags ȫ�������ڳ����ⲿ ���ֱ���й�������ʱ�޸�feature flags״̬����״̬�ڳ�������������Ч
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