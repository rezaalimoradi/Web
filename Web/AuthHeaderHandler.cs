using System.Net.Http.Headers;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly TokenProvider _tokenProvider;

    public AuthHeaderHandler(TokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // اجازه بده login بدون header اجرا شود
        if (!request.RequestUri!.AbsolutePath.Contains("/api/auth/login"))
        {
            // **Do NOT await InitializeAsync here** (it may call JS during prerender)
            var token = _tokenProvider.Token; // فقط از حافظه بخوان

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
