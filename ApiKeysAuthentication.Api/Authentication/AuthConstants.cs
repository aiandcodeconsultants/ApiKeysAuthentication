namespace ApiKeysAuthentication.Api.Authentication;

/// <summary>
/// A class that contains the constants used in the authentication.
/// </summary>
public static class AuthConstants
{
    /// <summary>
    /// The name of the authentication scheme.
    /// </summary>
    public const string ApiKeysSectionName = "Authentication:ApiKeys";

    /// <summary>
    /// The key to the api-key roles section of the configuration.
    /// </summary>
    public const string ApiKeyRolesSectionName = "Authentication:ApiKeyRoles";
}