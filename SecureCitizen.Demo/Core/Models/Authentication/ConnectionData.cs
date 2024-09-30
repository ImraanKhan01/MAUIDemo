namespace SecureCitizen.Demo.Core.Models.Authentication;

public class ConnectionData
{
    public string id { get; set; }
    public string type { get; set; }
    public object did { get; set; }
    public string imageUrl { get; set; }
    public string label { get; set; }
    public List<string> recipientKeys { get; set; }
    public object routingKeys { get; set; }
    public string serviceEndpoint { get; set; }
}