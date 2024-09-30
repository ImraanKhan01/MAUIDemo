using System.Text.Json.Serialization;

namespace SecureCitizen.Demo.Core.Models.Authentication;

public class TokenResponse
{
    public string id_token { get; set; }
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    
    public long expires_in { get; set; }
    public string token_type { get; set; }
    public string scope { get; set; }
}