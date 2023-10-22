using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using GivingGifts.SharedKernel.Core.Attributes;

namespace GivingGifts.SharedKernel.Core.JsonConverters;

internal class SensitiveDataConverter : JsonConverter<object>
{
    private readonly string[] _possiblePropertyNames =
    {
        "Email", "Password", "CardNumber", "SSN", "CVV"
    };

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new object();
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        var type = value.GetType();
        var properties = type.GetProperties();

        writer.WriteStartObject();

        foreach (var property in properties)
        {
            if (_possiblePropertyNames.Any(p =>
                    string.Equals(p, property.Name, StringComparison.CurrentCultureIgnoreCase))
                || property.GetCustomAttribute<SensitiveDataAttribute>() != null)
            {
                writer.WriteString(property.Name, "*SENSITIVE DATA*");
            }
            else
            {
                writer.WritePropertyName(property.Name);
                JsonSerializer.Serialize(writer, property.GetValue(value));
            }
        }

        writer.WriteEndObject();
    }
}