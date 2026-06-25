using Globomantics.Api.ApiServices;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Globomantics.Api.Authorization
{
    public class IsInRoleHandler(IAuthorizationApiService authorizationApiService) : AuthorizationHandler<IsInRoleRequirement> {
        private readonly IAuthorizationApiService _AuthorizationApiService = authorizationApiService;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            IsInRoleRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var permissions = await _AuthorizationApiService
                .GetPermissions(int.Parse(userId), requirement.ApplicationId);

            if (permissions.Role == requirement.Role)
                context.Succeed(requirement);
        }
    }
}

