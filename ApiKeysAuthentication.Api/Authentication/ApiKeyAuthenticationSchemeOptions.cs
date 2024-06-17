using Microsoft.AspNetCore.Authentication;

namespace ApiKeysAuthentication.Api.Authentication;

public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ApiKeyAuthentication";
    public const string AuthorizationHeaderName = "x-api-key";
}