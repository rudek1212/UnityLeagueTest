using System.Net.Http.Headers;
using System.Text;

namespace UnityLeagueTest;

public class PreserveAuthorizationRedirectHandler : DelegatingHandler
{
    private readonly AuthenticationHeaderValue _authHeader;

    public PreserveAuthorizationRedirectHandler(string username, string password)
    {
        _authHeader = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = _authHeader;

        var response = await base.SendAsync(request, cancellationToken);

        if ((int)response.StatusCode is 301 or 302 or 307 or 308)
        {
            var redirectUri = response.Headers.Location;
            if (redirectUri != null)
            {
                var newUri = redirectUri.IsAbsoluteUri
                    ? redirectUri
                    : new Uri(request.RequestUri!, redirectUri);

                var newRequest = new HttpRequestMessage(request.Method, newUri);
                newRequest.Headers.Authorization = _authHeader;

                foreach (var header in request.Headers)
                {
                    if (!newRequest.Headers.Contains(header.Key))
                        newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                response.Dispose();
                return await base.SendAsync(newRequest, cancellationToken);
            }
        }

        return response;
    }
}
