using Microsoft.Extensions.Configuration;

namespace Helper.AppSetting
{
    public static class AppSettings
    {
        private static IConfiguration _configuration;

        private static IConfiguration Configuration => _configuration ??= new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();


        public static string BackEndUrl => Configuration.GetSection("BackendUrl").Value;
        public static string ImgUrl => Configuration.GetSection("ImgUrl").Value;
        public static string SecurityKey => Configuration.GetSection("TokenOptions:SecurityKey").Value;
        public static string Environment => Configuration.GetSection("Application:Environment").Value;
        public static bool IsTestApplication => Configuration.GetSection("Application:Name").Value.Contains("test", StringComparison.InvariantCultureIgnoreCase);
    }
}