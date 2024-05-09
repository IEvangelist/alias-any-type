namespace Alias.AnyType.ResponseModels;

public record class GeoCode(
    string? Continent,
    string? ContinentCode,
    string? CountryName,
    string? CountryCode,
    string? City,
    string? Locality,
    string? PlusCode,
    LocalityInfo LocalityInfo)
{
    public override string ToString()
    {
        StringBuilder builder = new();

        var appended = ConditionalAppendFormat(builder, City);
        appended = ConditionalAppendFormat(builder, CountryName, appended ? ", {0}" : "{0}");
        appended = ConditionalAppendFormat(builder, LocalityInfo?.Informative?[0].Name, appended ? " {0}" : "{0}");
        _ = ConditionalAppendFormat(builder, LocalityInfo?.Informative?[0].Description, appended ? " — {0}" : "{0}");

        return builder.ToString();

        static bool ConditionalAppendFormat(
            StringBuilder builder,
            string? value,
            string? format = default)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                builder.Append(value);
            }
            else
            {
                builder.AppendFormat(format, value);
            }

            return true;
        }
    }
}
