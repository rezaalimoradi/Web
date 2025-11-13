using System.Net.Http.Headers;
using Web;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly TokenProvider _tokenProvider;

    public AuthHeaderHandler(TokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // مسیر login را استثناء کن
        if (!request.RequestUri!.AbsolutePath.Contains("/api/auth/login"))
        {
            await _tokenProvider.InitializeAsync();

            if (_tokenProvider.HasToken)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenProvider.Token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
