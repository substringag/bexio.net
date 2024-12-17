using System.Text.Json;
using System.Text.Json.Serialization;
using bexio.net.Models.Sales.Positions;

namespace bexio.net.Converter
{
    /// <summary>
    /// This class allows the serializer to convert Position-Json-Objects
    /// based on the "Type" string property.
    /// </summary>
    public class InvoicePositionsConverter : JsonConverter<PositionBase>
    {
        public override PositionBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // in making a copy, we get a new object, where the stream position is still at the start
            Utf8JsonReader readerCopy = reader;

            // read the 'Type'
            var dummy = JsonSerializer.Deserialize<Dummy>(ref reader, options);

            return dummy?.Type switch
            {
                PositionTypes.Custom   => JsonSerializer.Deserialize<PositionCustomExtended>(ref readerCopy, options),
                PositionTypes.Article  => JsonSerializer.Deserialize<PositionArticleExtended>(ref readerCopy, options),
                PositionTypes.Text     => JsonSerializer.Deserialize<PositionTextExtended>(ref readerCopy, options),
                PositionTypes.Subtotal => JsonSerializer.Deserialize<PositionSubtotalExtended>(ref readerCopy, options),
                PositionTypes.Pagebreak => JsonSerializer.Deserialize<PositionPagebreakExtended>(ref readerCopy,
                    options),
                PositionTypes.Discount => JsonSerializer.Deserialize<PositionDiscountExtended>(ref readerCopy, options),
                _                      => throw new JsonException()
            };
        }

        public override void Write(Utf8JsonWriter writer, PositionBase value, JsonSerializerOptions options)
        {
            // We need to cast here, so that we dont cause a recursive loop, when
            // this converter is used again and again...
            switch (value.Type)
            {
                case PositionTypes.Custom:
                    JsonSerializer.Serialize(writer, value as PositionCustomExtended, options);
                    break;
                case PositionTypes.Article:
                    JsonSerializer.Serialize(writer, value as PositionArticleExtended, options);
                    break;
                case PositionTypes.Text:
                    JsonSerializer.Serialize(writer, value as PositionTextExtended, options);
                    break;
                case PositionTypes.Subtotal:
                    JsonSerializer.Serialize(writer, value as PositionSubtotalExtended, options);
                    break;
                case PositionTypes.Pagebreak:
                    JsonSerializer.Serialize(writer, value as PositionPagebreakExtended, options);
                    break;
                case PositionTypes.Discount:
                    JsonSerializer.Serialize(writer, value as PositionDiscountExtended, options);
                    break;
                default:
                    throw new JsonException();
            }
        }

        /// <summary>
        /// This class is required for deserialization, because the base class 'PositionBase'
        /// does not have a parameterless constructor.
        /// </summary>
        private class Dummy : PositionBase
        {
        }
    }
}
