using SecureCitizen.Demo.Data.Interfaces;
using SecureCitizen.Demo.Data.Models;

namespace SecureCitizen.Demo.Data.Services;

public class TestApiService : ITestApiService
{
    private readonly ITestApiRepository _testApiRespositry;
    public TestApiService(ITestApiRepository testApiRepository)
    {
        _testApiRespositry = testApiRepository;
    }
    public async Task<List<Claim>> TestGetClaimsAsync()
    {
        return await this._testApiRespositry.TestGetClaimsAsync();
    }
}