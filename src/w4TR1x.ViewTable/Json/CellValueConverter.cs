using System.Text.Json;
using w4TR1x.ViewTable.Values;

namespace w4TR1x.ViewTable.Json;

public class CellValueConverter : JsonConverter<CellValue>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(CellValue).IsAssignableFrom(typeToConvert);
    }

    public override CellValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);

        //https://makolyte.com/csharp-deserialize-json-to-a-derived-type/
        //if the property isn't there, let it blow up

        var type = jsonDoc.RootElement.GetProperty("t").GetString();
        var value = jsonDoc.RootElement.GetProperty("v");

        switch (type)
        {
            case nameof(DateTimeValue):
                return new DateTimeValue(DateTime.Parse(value.GetProperty("value").GetString()!));

            case nameof(DateValue):
                return new DateValue(DateTime.Parse(value.GetProperty("value").GetString()!));

            case nameof(DecoratedDoubleValue):
                return ConstructDecoratedDoubleValue(value);

            case nameof(DoubleValue):
                return ConstructDoubleValue(value);

            case nameof(IntValue):
                return new IntValue(value.GetProperty("value").GetInt32());

            case nameof(StringValue):
                return new StringValue(value.GetProperty("value").GetString());

            case nameof(TimeValue):
                return new TimeValue(TimeSpan.Parse(value.GetProperty("value").GetString()!));

            //warning: If you're not using the JsonConverter attribute approach,
            //make a copy of options without this converter
            default:
                throw new JsonException("'Type' doesn't match a known derived type");
        }
    }

    public override void Write(Utf8JsonWriter writer, CellValue cellValue, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, new
        {
            t = cellValue.GetType().Name,
            v = (object)cellValue
        }, options);
        //warning: If you're not using the JsonConverter attribute approach,
        //make a copy of options without this converter
    }

    static DoubleValue ConstructDoubleValue(JsonElement value)
    {
        int calculateColumnInt = (int)CalculateStyleEnum.None;

        if (value.TryGetProperty("calculateColumn", out var calculateColumn))
        {
            calculateColumn.TryGetInt32(out calculateColumnInt);
        }

        int decimalCountInt = (int)ValueEnum.Int;

        if (value.TryGetProperty("decimalCount", out var decimalCount))
        {
            decimalCount.TryGetInt32(out decimalCountInt);
        }

        int zeroDecimalCountInt = (int)ValueEnum.Triple;

        if (value.TryGetProperty("zeroDecimalCount", out var zeroDecimalCount))
        {
            zeroDecimalCount.TryGetInt32(out zeroDecimalCountInt);
        }

        bool displayThousandSeparatorBool = false;

        if (value.TryGetProperty("displayThousandSeparator", out var displayThousandSeparator))
        {
            displayThousandSeparatorBool = displayThousandSeparator.GetBoolean();
        }

        bool alwaysDisplayDecimalsBool = false;

        if (value.TryGetProperty("alwaysDisplayDecimals", out var alwaysDisplayDecimals))
        {
            alwaysDisplayDecimalsBool = alwaysDisplayDecimals.GetBoolean();
        }

        return new DoubleValue(
                    value.GetProperty("value").GetDouble(),
                    (ValueEnum)decimalCountInt,
                    (ValueEnum)zeroDecimalCountInt,
                    displayThousandSeparatorBool,
                    alwaysDisplayDecimalsBool)
        {
            CalculateColumn = (CalculateStyleEnum)calculateColumnInt
        };
    }

    static DoubleValue ConstructDecoratedDoubleValue(JsonElement value)
    {
        string? beforeTextString = null;

        if (value.TryGetProperty("beforeText", out var beforeText))
        {
            beforeTextString = beforeText.GetString();
        }

        string? afterTextString = null;

        if (value.TryGetProperty("afterText", out var afterText))
        {
            afterTextString = afterText.GetString();
        }

        int calculateColumnInt = (int)CalculateStyleEnum.None;

        if (value.TryGetProperty("calculateColumn", out var calculateColumn))
        {
            calculateColumn.TryGetInt32(out calculateColumnInt);
        }

        int decimalCountInt = (int)ValueEnum.Int;

        if (value.TryGetProperty("decimalCount", out var decimalCount))
        {
            decimalCount.TryGetInt32(out decimalCountInt);
        }

        int zeroDecimalCountInt = (int)ValueEnum.Triple;

        if (value.TryGetProperty("zeroDecimalCount", out var zeroDecimalCount))
        {
            zeroDecimalCount.TryGetInt32(out zeroDecimalCountInt);
        }

        bool displayThousandSeparatorBool = false;

        if (value.TryGetProperty("displayThousandSeparator", out var displayThousandSeparator))
        {
            displayThousandSeparatorBool = displayThousandSeparator.GetBoolean();
        }

        bool alwaysDisplayDecimalsBool = false;

        if (value.TryGetProperty("alwaysDisplayDecimals", out var alwaysDisplayDecimals))
        {
            alwaysDisplayDecimalsBool = alwaysDisplayDecimals.GetBoolean();
        }

        return new DecoratedDoubleValue(
                    value.GetProperty("value").GetDouble(),
                    beforeTextString,
                    afterTextString,
                    (ValueEnum)decimalCountInt,
                    (ValueEnum)zeroDecimalCountInt,
                    displayThousandSeparatorBool,
                    alwaysDisplayDecimalsBool)
        {
            CalculateColumn = (CalculateStyleEnum)calculateColumnInt
        };
    }
}
