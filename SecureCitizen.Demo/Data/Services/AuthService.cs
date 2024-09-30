using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using SecureCitizen.Demo.Core.Helpers;
using SecureCitizen.Demo.Core.Models.Authentication;
using SecureCitizen.Demo.Data.Interfaces;

namespace SecureCitizen.Demo.Data.Services;

public class AuthService : IAuthService
{
    public async Task LoginAsync()
    {
        await AuthenticateUserAsync();
    }

    public async Task RefreshTokenAsync(string refreshToken)
    {
            var tokenResponse = await new HttpClient().PostAsync(Settings.TokenUrl, new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", Settings.ClientId),
                new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            }));

            if (tokenResponse.IsSuccessStatusCode)
            {
                var content = await tokenResponse.Content.ReadAsStringAsync();
                var tokenObject = JsonConvert.DeserializeObject<TokenResponse>(content);

                // Store the new access token and refresh token
                Session.AccessToken = tokenObject.access_token;

                // If a new refresh token is issued, replace the old one
                if (!string.IsNullOrEmpty(tokenObject.refresh_token))
                {
                    Session.RefreshToken = tokenObject.refresh_token;
                }

                await Session.SaveSessionAsync();
            }
            else
            {
                throw new Exception($"Token refresh failed: {tokenResponse.StatusCode}");
            }
        
    }

    public async Task<bool> IsTokenExpiredAsync()
    {
        var tokenExpiryTime = await Session.GetTokenExpiry();
        var buffer = TimeSpan.FromMinutes(5);
        return DateTime.UtcNow >= tokenExpiryTime - buffer;
    }
    
    private  async Task AuthenticateUserAsync()
    {
        var codeVerifier = PKCEHelper.GenerateCodeVerifier();
        var codeChallenge = PKCEHelper.GenerateCodeChallenge(codeVerifier);
        var codeChallengeMethod = "S256";

        var authUrl = new Uri($"{Settings.AuthUrl}" +
                              $"?client_id={Settings.ClientId}" +
                              $"&response_type={Settings.ResponseType}" +
                              $"&scope={Settings.Scopes}" +
                              $"&code_challenge={codeChallenge}" +
                              $"&code_challenge_method={codeChallengeMethod}" +
                              $"&redirect_uri={Settings.RedirectUri}");

        var callbackUrl = new Uri($"{Settings.RedirectUri}");
        try

        {
            // Get authorization code
            var authResult = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
            var authorizationCode = authResult.Properties["code"];

            // Exchange the authorization code for access token
            await PKCEHelper.ExchangeCodeForTokens(authorizationCode, codeVerifier, Settings.ClientId, Settings.RedirectUri);
        }
        catch
            (Exception ex)
        {
            Console.WriteLine("Authentication failed: " + ex.Message);
        }
    }
}