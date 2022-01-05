using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bexio.net.Converter
{
    /// <summary>
    /// This Converter can read any property type and convert it to a string.
    /// This is useful for amount values that are returned by the api as either
    /// 0  (integer/number)
    /// or
    /// "0.00"  (string)
    /// </summary>
    internal class AnyToStringConverter : JsonConverter<string>
    {
        public override string Read(
            ref Utf8JsonReader    reader,
            Type                  typeToConvert,
            JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            return jsonDoc.RootElement.ToString() ?? string.Empty;
        }

        public override void Write(
            Utf8JsonWriter        writer,
            string                value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
