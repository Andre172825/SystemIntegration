using Microsoft.Extensions.Configuration;

namespace SystemsIntegration.Api.Configurations.appSetting
{
    public class AppConfig: IAppConfig
    {

        public readonly IConfiguration _configuration;

        public AppConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string UnishopKey => _configuration.GetSection("ApiRiqra:UnishopKey").Value;
        public string UriRiqra => _configuration.GetSection("ApiRiqra:DominioApiRiqra").Value;
        public string SatelliteContext => _configuration.GetSection("ConnectionStrings:SatelliteContext").Value;
        public string SpringContext => _configuration.GetSection("ConnectionStrings:SpringContext").Value;
        public string RiqraWebHookKey_B2B => _configuration.GetSection("ApiRiqra:RiqraWebHookKey-B2B").Value;
        public string RiqraWebHookKey_B2C => _configuration.GetSection("ApiRiqra:RiqraWebHookKey-B2C").Value;
    }
}
