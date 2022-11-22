using Newtonsoft.Json;
using System.Globalization;

namespace Timeboxed.Core.Converters
{
    public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return string.IsNullOrWhiteSpace(value)
                ? null
                : DateOnly.ParseExact(value, Format, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer) =>
            writer.WriteValue(value.HasValue ? value.Value.ToString(Format, CultureInfo.InvariantCulture) : null);
    }
}
