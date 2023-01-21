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

        public static string FrontEndUrl => Configuration.GetSection("FrontEndUrl").Value;
        public static string BackEndUrl => Configuration.GetSection("BackEndUrl").Value;
        public static string SecurityKey => Configuration.GetSection("TokenOptions:SecurityKey").Value;
        public static string ImzaPosFrontEndUrl => Configuration.GetSection("ImzaPosFrontEndUrl").Value;
        public static string B2BUrl => Configuration.GetSection("B2BUrl").Value;
        public static string AsyB2BUrl => Configuration.GetSection("AsyB2BUrl").Value;
        public static string ImzaPosB2BUrl => Configuration.GetSection("ImzaPosB2BUrl").Value;
        public static string SupportImgUrl => Configuration.GetSection("SupportImgUrl").Value;
        public static string TenantLogoUrl => Configuration.GetSection("TenantLogoUrl").Value;
        public static string BackEndExcelUrl => Configuration.GetSection("BackEndExcelUrl").Value;
        public static string CurrencyUrl => Configuration.GetSection("CurrencyUrl").Value;
        public static string BakiyemFrontEndUrl => Configuration.GetSection("BakiyemFrontEndUrl").Value;
        public static string Environment => Configuration.GetSection("Application:Environment").Value;
        public static bool IsTestApplication => Configuration.GetSection("Application:Name").Value.Contains("test", StringComparison.InvariantCultureIgnoreCase);
    }
}