using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Globomantics.Idp.Pages.ServerSideSessions
{
    public class IndexModel : PageModel
    {
        private readonly ISessionManagementService _sessionManagementService;

        public IndexModel(ISessionManagementService sessionManagementService = null)
        {
            _sessionManagementService = sessionManagementService;
        }

        public QueryResult<UserSession> UserSessions { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Token { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Prev { get; set; }

        public async Task OnGet(CancellationToken ct)
        {
            if (_sessionManagementService != null)
            {
                UserSessions = await _sessionManagementService.QuerySessionsAsync(new SessionQuery
                {
                    ResultsToken = Token,
                    RequestPriorResults = Prev == "true",
                    DisplayName = Filter,
                    SessionId = Filter,
                    SubjectId = Filter,
                }, ct);
            }
        }

        [BindProperty]
        public string SessionId { get; set; }

        public async Task<IActionResult> OnPost(CancellationToken ct)
        {
            await _sessionManagementService.RemoveSessionsAsync(new RemoveSessionsContext
            {
                SessionId = SessionId,
            }, ct);
            return RedirectToPage("/ServerSideSessions/Index", new { Token, Filter, Prev });
        }
    }
}
