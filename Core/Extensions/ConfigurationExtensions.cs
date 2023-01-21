using Helper.Extentions;
using Microsoft.Extensions.Configuration;

namespace Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetDecryptedConnectionString(this IConfiguration configuration, string connectionName)
        {
            var connectionString = configuration.GetConnectionString(connectionName);

            var connectionPassword = configuration.GetSection("ConnectionKey").Value;
            if (!string.IsNullOrEmpty(connectionPassword))
            {
                connectionPassword = CryptoExtension.DecryptText(connectionPassword, "RocketTeamRayon");
            }

            return connectionString.Replace("{{PASS}}", connectionPassword);
        }
    }
}