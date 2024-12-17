using System.Text.Json;
using System.Text.Json.Serialization;
using bexio.net.Models.Sales.Repetition;

namespace bexio.net.Converter
{
    public class OrderRepetitionConverter : JsonConverter<OrderRepetitionIntervalBase>
    {
        public override OrderRepetitionIntervalBase? Read(ref Utf8JsonReader    reader,
                                                          Type                  typeToConvert,
                                                          JsonSerializerOptions options)
        {
            // in making a copy, we get a new object, where the stream position is still at the start
            Utf8JsonReader readerCopy = reader;

            // read the 'Type'
            var dummy = JsonSerializer.Deserialize<Dummy>(ref reader, options);

            return dummy?.Type switch
            {
                OrderRepetitionIntervals.Daily => JsonSerializer.Deserialize<OrderRepetitionDaily>(ref readerCopy,
                    options),
                OrderRepetitionIntervals.Weekly => JsonSerializer.Deserialize<OrderRepetitionWeekly>(ref readerCopy,
                    options),
                OrderRepetitionIntervals.Monthly => JsonSerializer.Deserialize<OrderRepetitionMonthly>(ref readerCopy,
                    options),
                OrderRepetitionIntervals.Yearly => JsonSerializer.Deserialize<OrderRepetitionYearly>(ref readerCopy,
                    options),
                _ => throw new JsonException()
            };
        }

        public override void Write(Utf8JsonWriter              writer,
                                   OrderRepetitionIntervalBase value,
                                   JsonSerializerOptions       options)
        {
            JsonSerializer.Serialize(writer, value);
        }

        /// <summary>
        /// This class is required for deserialization, because the base class 'PositionBase'
        /// does not have a parameterless constructor.
        /// </summary>
        private class Dummy : OrderRepetitionIntervalBase
        {
        }
    }
}
