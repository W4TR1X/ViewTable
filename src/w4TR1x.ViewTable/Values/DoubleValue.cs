using System.Globalization;

namespace w4TR1x.ViewTable.Values;

public class DoubleValue : CellValue<double>
{
    public CalculateStyleEnum CalculateColumn { get; set; }

    [JsonIgnore]
    public override dynamic? Value { get => base.Value; protected set => base.Value = value; }

    [JsonPropertyName("value")]
    public double OriginalValue { get; private set; }

    [JsonIgnore]
    private readonly CultureInfo _cultureInfo;

    [JsonIgnore]
    public string NumberFormat { get; private set; } = "0";

    public ValueEnum DecimalCount { get; }
    public ValueEnum ZeroDecimalCount { get; }

    public bool DisplayThousandSeparator { get; }
    public bool AlwaysDisplayDecimals { get; }

    [JsonConstructor]
    public DoubleValue(double value, ValueEnum decimalCount = ValueEnum.Int, ValueEnum zeroDecimalCount = ValueEnum.Triple,
                       bool displayThousandSeparator = false, bool alwaysDisplayDecimals = false)
        : base(Math.Round(value, (int)decimalCount))
    {
        OriginalValue = value;

        DecimalCount = decimalCount;
        ZeroDecimalCount = zeroDecimalCount;

        DisplayThousandSeparator = displayThousandSeparator;
        AlwaysDisplayDecimals = alwaysDisplayDecimals;

        _cultureInfo = new CultureInfo("tr-TR", false);

        Init();
    }

    private void Init()
    {
        var integerPart = (int)Math.Truncate(OriginalValue);
        var decimalPart = (float)(OriginalValue - integerPart);

        int tmpValue;

        if (integerPart == 0 || AlwaysDisplayDecimals)
        {
            switch (ZeroDecimalCount)
            {
                case ValueEnum.Auto:
                case ValueEnum.Triple when AlwaysDisplayDecimals:
                case ValueEnum.Double when AlwaysDisplayDecimals:
                case ValueEnum.Single when AlwaysDisplayDecimals:

                    tmpValue = (int)(decimalPart * 1000);
                    if ((tmpValue <= -1 && tmpValue >= -9) || (tmpValue >= 1 && tmpValue <= 9) || (ZeroDecimalCount==ValueEnum.Triple && AlwaysDisplayDecimals))
                    {
                        goto case ValueEnum.Triple;
                    }

                    tmpValue = (int)(decimalPart * 100);
                    if ((tmpValue <= -1 && tmpValue >= -9) || (tmpValue >= 1 && tmpValue <= 9) || (ZeroDecimalCount == ValueEnum.Double && AlwaysDisplayDecimals))
                    {
                        goto case ValueEnum.Double;
                    }

                    tmpValue = (int)(decimalPart * 10);
                    if ((tmpValue <= -1 && tmpValue >= -9) || (tmpValue >= 1 && tmpValue <= 9) || (ZeroDecimalCount == ValueEnum.Single && AlwaysDisplayDecimals))
                    {
                        goto case ValueEnum.Single;
                    }

                    goto default;

                case ValueEnum.Triple:

                    _cultureInfo.NumberFormat.NumberDecimalDigits = 3;
                    NumberFormat = "0.000";
                    break;

                case ValueEnum.Double:

                    _cultureInfo.NumberFormat.NumberDecimalDigits = 2;
                    NumberFormat = "0.00";
                    break;

                case ValueEnum.Single:

                    _cultureInfo.NumberFormat.NumberDecimalDigits = 1;
                    NumberFormat = "0.0";
                    break;

                default:
                    _cultureInfo.NumberFormat.NumberDecimalDigits = 0;
                    NumberFormat = "0";
                    break;
            }
        }
        else
        {
            switch (DecimalCount)
            {
                case ValueEnum.Triple:
                    NumberFormat = "0.000";
                    break;
                case ValueEnum.Double:
                    NumberFormat = "0.00";
                    break;
                case ValueEnum.Single:
                    NumberFormat = "0.0";
                    break;
                case ValueEnum.Int:
                    NumberFormat = "0";
                    break;
            }
            _cultureInfo.NumberFormat.NumberDecimalDigits = (int)DecimalCount;
        }

        _cultureInfo.NumberFormat.NumberGroupSeparator = "";
        if (DisplayThousandSeparator)
        {
            _cultureInfo.NumberFormat.NumberGroupSeparator = ".";
            NumberFormat = "#,##" + NumberFormat;
        }

        Value = Math.Round(OriginalValue, _cultureInfo.NumberFormat.NumberDecimalDigits);
    }

    public static DoubleValue Display3DecimalsWhenZeroWithSeparator(double value)
    {
        return new DoubleValue(value, ValueEnum.Int, ValueEnum.Triple, true);
    }

    public override dynamic AsOrderValue()
    {
        return OriginalValue;
    }
    public override string ToString()
    {
        return ((double)Value).ToString("N", _cultureInfo);
    }

    protected override void SetValue(double value)
    {
        OriginalValue = value;
        Init();
    }
}
