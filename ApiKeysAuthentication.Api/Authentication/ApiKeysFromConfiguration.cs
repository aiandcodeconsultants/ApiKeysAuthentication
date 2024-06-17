namespace ApiKeysAuthentication.Api.Authentication;

using static AuthConstants;

/// <summary>
/// A concrete implementation of <see cref="IApiKeys"/> that retrieves API keys from configuration.
/// </summary>
/// <param name="configuration">The configuration to read from.</param>
#pragma warning disable S3358 // Ternary operators should not be nested
public class ApiKeysFromConfiguration(IConfiguration configuration) : IApiKeys
{
    private Dictionary<string, string>? ApiKeys => configuration.GetSectionValue<Dictionary<string, string>>(ApiKeysSectionName);

    private Dictionary<string, string[]>? ApiKeyRoles => configuration.GetSectionValue<Dictionary<string, string[]>>(ApiKeyRolesSectionName);

    /// <inheritdoc/>
    public Task<Option<string>> GetName(string apiKey)
        => Task.FromResult(
            ApiKeys != null && ApiKeys.TryGetKeyByValue(apiKey, out var name)
                ? Option<string>.Some(name!)
                : Option<string>.None());

    /// <inheritdoc/>
    public async Task<Option<string[]>> GetRoles(string apiKey)
        => ApiKeyRoles?.TryGetValue(apiKey, out var roles) ?? false
                ? Option<string[]>.Some(roles) // <= If we matched roles return those
                : (await GetName(apiKey)).IsSome()
                    ? Option<string[]>.Some([]) // <= If we have a name but no roles return an empty array
                    : Option<string[]>.None(); // <= Otherwise return none
}