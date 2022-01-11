using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bexio.net.Converter
{
    /// <summary>
    /// Since Bexio returns a number in string format or null,
    /// this Converter converts it to a decimal.
    /// <br/><br/>
    /// This only works with nullable decimal types. If non-nullable decimals
    /// are required, make a copy of this class, or use decimal? neverthless.
    /// </summary>
    public class NullableDecimalConverter : JsonConverter<decimal?>
    {
        public override decimal? Read(
            ref Utf8JsonReader    reader,
            Type                  typeToConvert,
            JsonSerializerOptions options)
        {
            // parsing the string like this (instead of reader.GetString()) allows us
            // to handle numbers like 0 as well as strings like "0.0"
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            string    str     = jsonDoc.RootElement.ToString() ?? string.Empty;

            // Read it as a decimal
            if (decimal.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out var d))
                return d;

            return null;
        }

        public override void Write(
            Utf8JsonWriter        writer,
            decimal?              value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(CultureInfo.InvariantCulture));
        }
    }
    /*public class NumberConverter : JsonConverter<Number>
    {
        public override Number Read(
            ref Utf8JsonReader    reader,
            Type                  typeToConvert,
            JsonSerializerOptions options)
        {
            return new Number(reader.GetString());
        }

        public override void Write(
            Utf8JsonWriter        writer,
            Number                value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }*/
}
