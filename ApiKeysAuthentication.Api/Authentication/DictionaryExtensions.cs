namespace ApiKeysAuthentication.Api.Authentication;

/// <summary>
/// A class that contains the extensions for dictionary objects.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// Tries to get the key by the value, and returns true if any match was found and the key is set.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to search in.</param>
    /// <param name="value">The value to search for.</param>
    /// <param name="key">The key that was found.</param>
    /// <returns>The value indicating whether the key was found.</returns>
    public static bool TryGetKeyByValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value, out TKey? key)
        where TKey : notnull
    {
        var matched = dictionary.Where(x => x.Value?.Equals(value) ?? false).Take(1).ToList();

        if (matched.Count == 1)
        {
            key = matched[0].Key;
            return true;
        }

        key = default;
        return false;
    }
}