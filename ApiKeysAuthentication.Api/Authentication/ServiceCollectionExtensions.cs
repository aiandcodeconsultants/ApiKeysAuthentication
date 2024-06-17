using System.Security.Cryptography.X509Certificates;

namespace ApiKeysAuthentication.Api.Authentication;

/// <summary>
/// A class that contains the extensions for the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiKeysAuthentication(this IServiceCollection services, string scheme = ApiKeyAuthenticationSchemeOptions.DefaultScheme, Action<ApiKeyAuthenticationSchemeOptions>? configure = null)
    {
        services
            .AddAuthentication(scheme)
            .AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
                scheme,
                configure);

        return services;
    }
}