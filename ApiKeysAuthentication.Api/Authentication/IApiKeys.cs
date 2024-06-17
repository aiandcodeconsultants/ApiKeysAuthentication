namespace ApiKeysAuthentication.Api.Authentication;

/// <summary>
/// An interface to provide API key information.
/// </summary>
public interface IApiKeys
{
    /// <summary>
    /// Gets the name of the API key.
    /// </summary>
    /// <param name="apiKey">The api key.</param>
    /// <returns>The name matching the API key, if matched, otherwise <see langword="null"/>.</returns>
    Task<Option<string>> GetName(string apiKey);

    /// <summary>
    /// Gets the roles associated with the API key.
    /// </summary>
    /// <param name="apiKey">The api key.</param>
    /// <returns>The roles assigned to the API key.</returns>
    Task<Option<string[]>> GetRoles(string apiKey);
}