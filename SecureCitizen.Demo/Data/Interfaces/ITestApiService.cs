using SecureCitizen.Demo.Data.Models;

namespace SecureCitizen.Demo.Data.Interfaces;

public interface ITestApiService
{
    public Task<List<Claim>> TestGetClaimsAsync();
}