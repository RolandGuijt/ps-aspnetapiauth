using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Globomantics.Idp.Pages.Account.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class LoggedOut(IIdentityServerInteractionService interactionService) : PageModel {
    private readonly IIdentityServerInteractionService _interactionService = interactionService;

    public LoggedOutViewModel View { get; set; }

    public async Task OnGet(string logoutId, CancellationToken ct)
    {
        // get context information (client name, post logout redirect URI and iframe for federated signout)
        var logout = await _interactionService.GetLogoutContextAsync(logoutId, ct);

        View = new LoggedOutViewModel
        {
            AutomaticRedirectAfterSignOut = LogoutOptions.AutomaticRedirectAfterSignOut,
            PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
            ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
            SignOutIframeUrl = logout?.SignOutIFrameUrl
        };
    }
}
