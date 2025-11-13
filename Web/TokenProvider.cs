using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class TokenProvider
{
    private readonly ProtectedSessionStorage _storage;
    private string? _token;
    private bool _initialized = false;

    public TokenProvider(ProtectedSessionStorage storage)
    {
        _storage = storage;
    }

    public string? Token => _token;
    public bool HasToken => !string.IsNullOrWhiteSpace(_token);

    public async Task InitializeAsync()
    {
        if (_initialized) return;

        try
        {
            // بررسی JS آماده بودن
            if (_storage is null) return;

            var result = await _storage.GetAsync<string>("AuthToken");
            if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
                _token = result.Value;
        }
        catch (Microsoft.JSInterop.JSException)
        {
            // JS هنوز آماده نیست → توکن null
            _token = null;
        }

        _initialized = true;
    }

    public async Task SetTokenAsync(string token)
    {
        _token = token;
        if (_storage != null)
            await _storage.SetAsync("AuthToken", token);
    }

    public async Task ClearAsync()
    {
        _token = null;
        if (_storage != null)
            await _storage.DeleteAsync("AuthToken");
    }
}
