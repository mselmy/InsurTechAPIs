using InsurTech.Core.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsurTech.APIs.CustomeValidation
{
    public class InsurancePlanLevelConverter : JsonConverter<InsurancePlanLevel>
    {
        public override InsurancePlanLevel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string enumString = reader.GetString();
                if (Enum.TryParse(enumString, true, out InsurancePlanLevel level))
                {
                    return level;
                }
            }
            if (reader.TokenType == JsonTokenType.Number)
            {
                int enumInt = reader.GetInt32();
                if (Enum.IsDefined(typeof(InsurancePlanLevel), enumInt))
                {
                    return (InsurancePlanLevel)enumInt;
                }
            }
            throw new JsonException($"Unable to convert {reader.GetString()} to InsurancePlanLevel");
        }

        public override void Write(Utf8JsonWriter writer, InsurancePlanLevel value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
