namespace SystemsIntegration.Api.Configurations.appSetting
{
    public interface IAppConfig
    {
        string UnishopKey { get; }
        string UriRiqra { get; }
        string SatelliteContext { get; }
        string SpringContext { get; }
        string RiqraWebHookKey_B2B { get; }
        string RiqraWebHookKey_B2C { get; }
    }
}
