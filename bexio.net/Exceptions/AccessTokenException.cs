namespace bexio.net.Exceptions;

/// <summary>
/// No access token could be obtained, so the request was never sent. Always thrown, regardless
/// of <see cref="UnsuccessfulReturnStyle"/> — a missing connection must not look like an empty result.
/// </summary>
public class AccessTokenException : Exception
{
    public AccessTokenException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
