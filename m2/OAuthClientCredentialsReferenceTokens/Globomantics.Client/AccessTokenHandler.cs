using Duende.IdentityModel.Client;

namespace Globomantics.Client;

public class AccessTokenHandler: DelegatingHandler
{
    private string? _AccessToken;
    private DateTimeOffset? _AccessTokenExpiration;
    private DiscoveryCache _DiscoClient = new("https://localhost:5001");
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await GetAccessToken();
        request.SetBearerToken(token);
        return await base.SendAsync(request, cancellationToken);
    }
    
    private async Task FetchAccessToken()
    {
        Console.WriteLine("Fetching access token");
        
        var disco = await _DiscoClient.GetAsync();

        var client = new HttpClient();
        var tokenResponse = await client.
            RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "m2m.client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                Scope = "globoapi_fullaccess"
            });
        
        _AccessToken = tokenResponse.AccessToken;
        _AccessTokenExpiration = 
            DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
    }
    private async Task<string?> GetAccessToken()
    {
        if (_AccessToken != null)
        {
            if (DateTime.UtcNow.AddMinutes(1) < _AccessTokenExpiration)
            {   
                Console.WriteLine("Using cached access token");
                return _AccessToken;
            }
        }
        
        await FetchAccessToken();
        return _AccessToken;
    }
}