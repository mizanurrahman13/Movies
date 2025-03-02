using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MOVIES.Api.Auth;

public class AdminAuthRequirement : IAuthorizationHandler, IAuthorizationRequirement
{
    private readonly string _apiKey;

    public AdminAuthRequirement(string apiKey)
    {
        _apiKey = apiKey;
    }

    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.User.HasClaim(AuthConstants.AdminUserClaimName, "true"))
        {
            context.Succeed(this);
            return Task.CompletedTask;
        }

        var httpContext = context.Resource as HttpContext;
        if (httpContext is null)
            return Task.CompletedTask;

        if (!httpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extactedApiKey))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if (_apiKey != extactedApiKey)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        var identity = (ClaimsIdentity)httpContext.User.Identity!;
        identity.AddClaim(new Claim("userid", Guid.Parse("3d58b199-7d8b-4e18-89c7-84f5efc2a554").ToString()));
        context.Succeed(this);

        return Task.CompletedTask;
    }
}
