using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using SecureCitizen.Demo.Core.Models.Authentication;

namespace SecureCitizen.Demo.Core.Helpers;

public static class PKCEHelper
{
    public static string GenerateCodeVerifier()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            return Base64UrlEncodeNoPadding(bytes);
        }
    }

    public static string GenerateCodeChallenge(string codeVerifier)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.ASCII.GetBytes(codeVerifier);
            var hash = sha256.ComputeHash(bytes);
            return Base64UrlEncodeNoPadding(hash);
        }
    }

    private static string Base64UrlEncodeNoPadding(byte[] bytes)
    {
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");
    }

    public static async Task ExchangeCodeForTokens(string code, string codeVerifier, string clientId, string redirectUri)
    {
        var tokenUrl = $"{Settings.TokenUrl}";

        var tokenResponse = await new HttpClient().PostAsync(tokenUrl, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("redirect_uri", redirectUri),
            new KeyValuePair<string, string>("code_verifier", codeVerifier)
        }));

        if (tokenResponse.IsSuccessStatusCode)
        {
            var content = await tokenResponse.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(content))
            {
                var tokenObject = JsonConvert.DeserializeObject<TokenResponse>(content);
                Session.AccessToken = tokenObject.access_token;
                Session.RefreshToken = tokenObject.refresh_token;
                Session.SetTokenExpiry(tokenObject.expires_in);;
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(tokenObject.access_token);
                Session.Username = jwtToken.Claims.First(claim => claim.Type == "name").Value;
                Session.Email = jwtToken.Claims.First(claim => claim.Type == "email").Value;
                await Session.SaveSessionAsync();
            }

            Console.WriteLine("Token response: " + content);
        }
        else
        {
            Console.WriteLine("Token exchange failed: " + tokenResponse.StatusCode);
        }
    }
    
}