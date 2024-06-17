namespace ApiKeysAuthentication.Api.Extensions;

/// <summary>
/// A class that contains extension methods for the <see cref="IConfiguration"/> interface.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Gets the configuration section as a structured value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to retrieve.</typeparam>
    /// <param name="configuration">The configuration to read from.</param>
    /// <param name="key">The key of the section to retrieve.</param>
    /// <returns>The value of the section as a structured value of type <typeparamref name="T"/>.</returns>
    public static T? GetSectionValue<T>(this IConfiguration configuration, string key)
        => configuration.GetSection(key).Get<T>();
}