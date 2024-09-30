using Newtonsoft.Json;
using SecureCitizen.Demo.Core.Models;
namespace SecureCitizen.Demo.Core.Helpers;

public static class Session
{
    public static string Username { get; set; }
    public static string Email { get; set; }
    public static string AccessToken { get; set; }
    
    public static string RefreshToken { get; set; }
    private static DateTime TokenExpiry {  get;  set; }
    
    public static bool IsAuthenticated()
    {
        if (!string.IsNullOrEmpty(AccessToken) & TokenExpiry > DateTime.UtcNow)
        {
            return true;
        }

        return false;
    }
    public  static async Task LoadSessionAsync()
    {
        var sessionString = await SecureStorage.GetAsync(StringHelper.SessionKey);
        if (!string.IsNullOrWhiteSpace(sessionString))
        {
            var session = JsonConvert.DeserializeObject<SessionParams>(sessionString);
            if (session != null)
            {
                Username = session.Username;
                Email = session.Email;
                AccessToken = session.AccessToken;
                TokenExpiry = session.TokenExpiry;
                RefreshToken = session.RefreshToken;
            }

            ;
        }
    }

    public static void SetTokenExpiry(long expiryTimeInSeconds)
    {
        double expirySeconds = expiryTimeInSeconds;
        TokenExpiry = DateTime.UtcNow.AddSeconds(expirySeconds);
    }
    
    public static async Task<DateTime> GetTokenExpiry()
    {
        return await Task.FromResult<DateTime>(TokenExpiry);
    }
    public  static async Task SaveSessionAsync()
    {
        var sessionParams = new SessionParams
        {
            Username = Username,
            AccessToken = AccessToken,
            TokenExpiry = TokenExpiry,
            RefreshToken = RefreshToken,
            Email = Email
        };
        var sessionJson = JsonConvert.SerializeObject(sessionParams);
        await SecureStorage.SetAsync(StringHelper.SessionKey, sessionJson);
    }

    public static async Task ClearSessionAsync()
    {
         SecureStorage.Remove(StringHelper.SessionKey);
    }
}