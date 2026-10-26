namespace bexio.net.Authentication;

/// <summary>
/// Supplies the bearer token for every request, so short-lived OAuth access tokens can be
/// refreshed transparently instead of being fixed at construction time.
/// </summary>
public interface IAccessTokenProvider
{
    /// <summary>Returns a currently valid access token. Called once per request.</summary>
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Called when bexio rejected the token with 401, so a cached token is not handed out again.
    /// </summary>
    void Invalidate();
}

/// <summary>A fixed token, e.g. a Personal Access Token from developer.bexio.com.</summary>
public class StaticAccessTokenProvider(string accessToken) : IAccessTokenProvider
{
    public Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(accessToken);

    public void Invalidate()
    {
    }
}
