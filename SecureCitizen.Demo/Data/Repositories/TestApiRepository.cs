using SecureCitizen.Demo.Core.Helpers;
using SecureCitizen.Demo.Data.Interfaces;
using SecureCitizen.Demo.Data.Models;

namespace SecureCitizen.Demo.Data.Repositories;

public class TestApiRepository : BaseRepository,ITestApiRepository
{
    private readonly IAuthService _authService;
    public TestApiRepository(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<List<Claim>> TestGetClaimsAsync()
    {
        try
        {
            var tokenExpired = await this._authService.IsTokenExpiredAsync();
            if (tokenExpired)
            {
                await this._authService.RefreshTokenAsync(Session.RefreshToken);
            }

            var response = await GetAsync<List<Claim>>(Settings.TestApiUrl);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine();
           return new List<Claim>();
        }
    }
}