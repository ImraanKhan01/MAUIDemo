using SecureCitizen.Demo.Data.Models;

namespace SecureCitizen.Demo.Data.Interfaces;

public interface ITestApiRepository
{
    public Task<List<Claim>> TestGetClaimsAsync();
}