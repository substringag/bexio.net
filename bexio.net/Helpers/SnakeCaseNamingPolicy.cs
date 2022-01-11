using System.Linq;
using System.Text.Json;

namespace bexio.net.Helpers
{
    /// <summary>
    ///  Policy that all our Json Properties are named in snake_case.
    /// </summary>
    internal class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            return ToSnakeCase(name);
        }

        public static string ToSnakeCase(string str)
        {
            return string.Concat(str
                    .Select((x, i) =>
                        i > 0 &&
                        char.IsUpper(x)
                            ? $"_{x}"
                            : x.ToString()))
                .ToLower();
        }
    }
}
