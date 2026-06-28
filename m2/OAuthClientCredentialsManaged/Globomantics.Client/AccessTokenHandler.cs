using Duende.IdentityModel.Client;

namespace Globomantics.Client;

public class AccessTokenHandler(IHttpClientFactory clientFactory): DelegatingHandler
{
    private string? _accessToken;
    private DateTimeOffset? _accessTokenExpiration;
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBearerToken(await GetAccessToken());
        return await base.SendAsync(request, cancellationToken);
    }
    
    private async Task FetchAccessToken()
    {
        Console.WriteLine("Fetching access token");
        var discoClient = new DiscoveryCache("https://localhost:5001");
        var disco = await discoClient.GetAsync();

        var client = new HttpClient();
        var tokenResponse = await client.
            RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "m2m.client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                Scope = "globoapi"
            });
        
        _accessToken = tokenResponse.AccessToken;
        _accessTokenExpiration = DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
    }
    private async Task<string?> GetAccessToken()
    {
        if (_accessToken != null)
        {
            if (DateTime.UtcNow.AddMinutes(1) < _accessTokenExpiration)
            {   Console.WriteLine("Using cached access token");
                return _accessToken;
            }
        }
        
        await FetchAccessToken();
        return _accessToken;
    }
}