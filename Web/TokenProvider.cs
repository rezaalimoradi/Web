using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;

public class TokenProvider
{
    private readonly ProtectedSessionStorage? _storage;
    private string? _token;
    private bool _initialized = false;

    public TokenProvider(ProtectedSessionStorage? storage)
    {
        _storage = storage;
    }

    public string? Token => _token;
    public bool HasToken => !string.IsNullOrWhiteSpace(_token);

    /// <summary>
    /// Safe initialize: tries to read from ProtectedSessionStorage, but catches JS exceptions.
    /// Should be called from a component's OnAfterRenderAsync (not from background threads).
    /// </summary>
    public async Task InitializeAsync()
    {
        if (_initialized) return;

        if (_storage == null)
        {
            _initialized = true;
            return;
        }

        try
        {
            var result = await _storage.GetAsync<string>("AuthToken");
            if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
                _token = result.Value;
        }
        catch (JSException)
        {
            // JS not ready (prerender). keep _token as null; don't rethrow.
            _token = null;
        }

        _initialized = true;
    }

    /// <summary>
    /// Set token in-memory and persist to session storage (if storage available).
    /// Call this after successful login from an interactive context (e.g. event handler or OnAfterRenderAsync).
    /// </summary>
    public async Task SetTokenAsync(string token)
    {
        _token = token;
        if (_storage != null)
        {
            try
            {
                await _storage.SetAsync("AuthToken", token);
            }
            catch (JSException)
            {
                // JS not ready — token stays in memory; a subsequent InitializeAsync (after JS ready) will store it if desired
            }
        }
    }

    public async Task ClearAsync()
    {
        _token = null;
        if (_storage != null)
        {
            try
            {
                await _storage.DeleteAsync("AuthToken");
            }
            catch (JSException)
            {
                // ignore
            }
        }
    }
}
