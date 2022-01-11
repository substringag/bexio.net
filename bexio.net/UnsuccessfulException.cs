using System;

namespace bexio.net
{
    public class UnsuccessfulException : Exception
    {
        public int StatusCode { get; }

        public UnsuccessfulException(int statusCode)
            : this(statusCode, $"The request was unsuccessful ({statusCode})")
        {
        }

        public UnsuccessfulException(int statusCode, string? message)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
