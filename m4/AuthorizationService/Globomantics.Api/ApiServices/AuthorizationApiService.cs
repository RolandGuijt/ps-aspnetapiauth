using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;

namespace Globomantics.Api.ApiServices
{
    public class AuthorizationApiService(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor contextAccessor) : IAuthorizationApiService {
        private readonly IHttpContextAccessor _ContextAccessor = contextAccessor;
        private HttpClient _HttpClient = httpClientFactory.CreateClient("authorization");
        public async Task<Permissions> GetPermissions(int userId, int applicationId)
        {
            var accessToken = await _ContextAccessor.
                HttpContext.GetTokenAsync("access_token");
            _HttpClient.SetBearerToken(accessToken);

            //Add caching here

            var response = await _HttpClient
                .GetAsync($"/user/{userId}?applicationId={applicationId}");
            return await response.Content.ReadFromJsonAsync<Permissions>();
        }
    }

    public class Permissions
    {
        public string Role { get; set; }
    }
}

