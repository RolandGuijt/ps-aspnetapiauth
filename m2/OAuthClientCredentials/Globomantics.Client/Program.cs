using Duende.IdentityModel.Client;

var discoClient = new DiscoveryCache("https://localhost:5001");
var disco = await discoClient.GetAsync();

var client = new HttpClient();

var tokenResponse = await client.RequestClientCredentialsTokenAsync(
    new ClientCredentialsTokenRequest
    {
        Address = disco.TokenEndpoint,

        ClientId = "m2m.client",
        ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
        Scope = "globoapi"
    });
    
if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.AccessToken);
Console.WriteLine(tokenResponse.ExpiresIn);

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:5002/conference");

response.EnsureSuccessStatusCode();

Console.WriteLine(await response.Content.ReadAsStringAsync());