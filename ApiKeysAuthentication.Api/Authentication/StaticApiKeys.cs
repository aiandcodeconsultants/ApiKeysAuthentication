namespace ApiKeysAuthentication.Api.Authentication;

/// <summary>
/// A static api key provider.
/// </summary>
/// <param name="nameApiKeyDictionary">The name to api key dictionary.</param>
/// <param name="apiKeyRoleDictionary">The api key to roles dictionary.</param>
public class StaticApiKeys(
    Dictionary<string, string> nameApiKeyDictionary,
    Dictionary<string, string[]> apiKeyRoleDictionary)
    : IApiKeys
{
    /// <inheritdoc/>
    public Task<Option<string>> GetName(string apiKey)
        => Task.FromResult(
            nameApiKeyDictionary.TryGetKeyByValue(apiKey, out var name)
                ? Option<string>.Some(name)
                : Option<string>.None());

    /// <inheritdoc/>
    public Task<Option<string[]>> GetRoles(string apiKey)
        => Task.FromResult(
            apiKeyRoleDictionary.TryGetValue(apiKey, out var roles)
                ? Option<string[]>.Some(roles)
                : Option<string[]>.None());
}