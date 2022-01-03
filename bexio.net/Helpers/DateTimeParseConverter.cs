using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bexio.net.Helpers
{
    /// <summary>
    /// This converter uses DateTime.Parse to convert a string to a DateTime.
    /// By default, System.Text.Json will use a custom conversion that does not work with the format, that Bexio uses.
    /// </summary>
    internal class DateTimeParseConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTime));
            return DateTime.Parse(reader.GetString() ?? throw new FormatException("DateTime string is null"));
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // 2022-01-01 12:00:00
            writer.WriteStringValue(value.ToString("u", CultureInfo.InvariantCulture).TrimEnd('Z'));
        }
    }
}
