
namespace SecureCitizen.Demo;

public static class Settings
{
    public static bool UseMock { get; set; } = true;

    public const string BaseUrl = "https://demo.duendesoftware.com";
    public const string AuthUrl = $"{BaseUrl}/connect/authorize";
    public const string TokenUrl = $"{BaseUrl}/connect/token";
    public const string TestApiUrl = $"{BaseUrl}/api/test";
    public const string ClientId = "interactive.confidential";
    public const string ClientSecret = "secret";
    public const string Scopes = "openid profile api email offline_access";
    public const string RedirectUri = "myapp://callback";
    public const string ResponseType = "code";
    

}
