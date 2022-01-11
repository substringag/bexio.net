namespace bexio.net.Helpers
{
    public static class QueryStringExtensions
    {
        public static string AddQueryParameter(this string url, string key, object? value)
        {
            return url + (url.Contains("?")
                ? $"&{key}={value}"
                : $"?{key}={value}");
        }
    }
}
