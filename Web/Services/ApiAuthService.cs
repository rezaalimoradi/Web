using System.Net.Http.Json;
using Web;

public class ApiAuthService
{
    private readonly HttpClient _httpNoAuth; // برای login بدون header
    private readonly HttpClient _http;       // برای request های محافظت‌شده
    private readonly TokenProvider _tokenProvider;

    public ApiAuthService(IHttpClientFactory httpFactory, TokenProvider tokenProvider)
    {
        _httpNoAuth = httpFactory.CreateClient("ApiClientNoAuth"); // فقط برای login
        _http = httpFactory.CreateClient("ApiClient");            // با AuthHeaderHandler
        _tokenProvider = tokenProvider;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var resp = await _httpNoAuth.PostAsJsonAsync("api/auth/login", new { username, password });

        if (!resp.IsSuccessStatusCode)
            return false;

        var obj = await resp.Content.ReadFromJsonAsync<LoginResult>();
        if (obj?.token != null)
        {
            await _tokenProvider.SetTokenAsync(obj.token); // ذخیره توکن در ProtectedSessionStorage
            return true;
        }

        return false;
    }

    public async Task LogoutAsync()
    {
        await _tokenProvider.ClearAsync();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        await _tokenProvider.InitializeAsync();
        return _tokenProvider.HasToken;
    }

    public class LoginResult
    {
        public string token { get; set; } = "";
        public int expiresIn { get; set; }
    }
}
