namespace SecureCitizen.Demo.Data.Interfaces;

public interface IAuthService
{
    public Task LoginAsync();
    public Task RefreshTokenAsync(string refreshToken);
    public Task<bool> IsTokenExpiredAsync();
}