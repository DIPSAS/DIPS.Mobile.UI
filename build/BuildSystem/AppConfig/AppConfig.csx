public class AppConfig
{
    public AppCenterAppConfig AppCenter{ get; set; }
}

public class AppCenterAppConfig
{
    public string ApiKey { get; set; }
    public string AppName { get; set; }
    public string DistributionGroup { get; set; }
}