namespace SecureCitizen.Demo.Core.Models;

public class SessionParams
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    
    public DateTime TokenExpiry { get; set; }
    public string RefreshToken { get; set; }
}