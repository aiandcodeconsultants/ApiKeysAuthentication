using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ApiKeysAuthentication.Api.Authentication;

public class ApiKeyAuthenticationHandler(
    IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> options,
    ILoggerFactory loggerFactory,
    UrlEncoder urlEncoder,
    IApiKeys apiKeys)
    : AuthenticationHandler<ApiKeyAuthenticationSchemeOptions>(options, loggerFactory, urlEncoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyAuthenticationSchemeOptions.AuthorizationHeaderName, out var authenticationHeaderValue))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        if (string.IsNullOrEmpty(authenticationHeaderValue))
        {
            return AuthenticateResult.NoResult();
        }

        var apiKey = authenticationHeaderValue[0]!;
        var identity = new ClaimsIdentity(Scheme.Name);

        var name = await apiKeys.GetName(apiKey);
        var roles = await apiKeys.GetRoles(apiKey);

        if (name.IsNone() && (roles.IsNone() || roles.Value().Length == 0))
            return AuthenticateResult.Fail("Unauthorized - Invalid API Key");

        if (name!.IsSome())
            identity.AddClaim(new Claim(ClaimTypes.Name, name.Value()));

        if (roles.IsSome())
            foreach (var role in roles.Value())
                identity.AddClaim(new Claim(ClaimTypes.Role, role));

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}
