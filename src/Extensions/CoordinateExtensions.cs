namespace Alias.AnyType.Extensions;

internal static class CoordinateExtensions
{
    internal static string ToCardinalizedString(this Coordinates coordinates)
    {
        var (latCardinalized, lonCardinalized) = (
            FormatCardinal(coordinates.Latitude, true),
            FormatCardinal(coordinates.Longitude, false)
        );

        return $"{latCardinalized},{lonCardinalized}";

        static string FormatCardinal(double degrees, bool isLat)
        {
            (int degree, int minute, double second) = degrees.ToDMS();

            var cardinal = degrees.ToCardinal(isLat);

            return $"{degree}°{minute}'{second % 60:F4}\"{cardinal}";
        }
    }

    internal static DMS ToDMS(this double coordinate)
    {
        var ts = TimeSpan.FromHours(Abs(coordinate));

        int degrees = (int)(Sign(coordinate) * Floor(ts.TotalHours));
        int minutes = ts.Minutes;
        double seconds = ts.TotalSeconds;

        return new DMS(degrees, minutes, seconds);
    }

    internal static char ToCardinal(this double degree, bool isLatitude) => isLatitude switch
    {
        true => degree >= 0 ? 'N' : 'S',
        _ => degree >= 0 ? 'E' : 'W'
    };
}
