internal static partial class Program
{
    static readonly double s_radiansOverDegrees = PI / 180.0;
    
    const double MetersInMile = 1_609.344;
    const double R = 6_376_500;

    static Coordinates GetRandomCoordinates(Random? random = default)
    {
        random ??= Random.Shared;

        double minLat = -90.0, maxLat = 90.0;
        double minLon = -180.0, maxLon = 180.0;

        // Generate random latitude and longitude
        double latitude = minLat + (maxLat - minLat) * random.NextDouble();
        double longitude = minLon + (maxLon - minLon) * random.NextDouble();

        return (
            Latitude: Round(latitude, 8),
            Longitude: Round(longitude, 8)
        );
    }

    // Borrowed from:
    //   https://github.com/microsoft/referencesource/blob/master/System.Device/GeoCoordinate.cs#L190-L239
    static Distance CalculateHaversineDistance(Coordinates one, Coordinates two)
    {
        //  The Haversine formula according to Dr. Math.
        //  http://mathforum.org/library/drmath/view/51879.html

        //  dlon = lon2 - lon1
        //  dlat = lat2 - lat1
        //  a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
        //  c = 2 * atan2(sqrt(a), sqrt(1-a)) 
        //  d = R * c

        //  Where
        //    * dlon is the change in longitude
        //    * dlat is the change in latitude
        //    * c is the great circle distance in Radians.
        //    * R is the radius of a spherical Earth.
        //    * The locations of the two points in 
        //        spherical coordinates (longitude and 
        //        latitude) are lon1,lat1 and lon2, lat2.

        var sLatitudeRadians = one.Latitude * s_radiansOverDegrees;
        var sLongitudeRadians = one.Longitude * s_radiansOverDegrees;
        var eLatitudeRadians = two.Latitude * s_radiansOverDegrees;
        var eLongitudeRadians = two.Longitude * s_radiansOverDegrees;

        var dLongitude = eLongitudeRadians - sLongitudeRadians;
        var dLatitude = eLatitudeRadians - sLatitudeRadians;

        var a = Pow(Sin(dLatitude / 2.0), 2.0) +
                     Cos(sLatitudeRadians) * Cos(eLatitudeRadians) *
                     Pow(Sin(dLongitude / 2.0), 2.0);

        var c = 2.0 * Atan2(Sqrt(a), Sqrt(1.0 - a));

        var distanceInMeters = R * c;
        var distanceInKilometers = distanceInMeters / 1_000;
        var distanceInMiles = distanceInMeters / MetersInMile;

        return (
            Meters: distanceInMeters,
            Kilometers: distanceInKilometers,
            Miles: distanceInMiles
        );
    }

    static void WriteIntroduction()
    {
        WriteLine($"""
            🛸 <-- UFO SIGHTINGS! --> 👾

            🙈 This is a demo app that fakes UFO sightings and shows how to use "alias any type" in C# 12.
            
            ❓ What the app does:
            
            ✨ Randomly generates valid coordinates.
            🛜 Requires a network connection (makes outgoing HTTP requests).
            🤓 Coordinates are represented as degrees-minutes-seconds (DMS) with cardinality.
            🔦 As spotters observe and report sightings, they're listed in the console.
            ➗ When multiple coordinates are reported, distance calculates are reported as alerts.

            PRESS ANY KEY TO START WATCHING FOR UFOs...
            """);
        WriteHyphens();

        ReadKey();
    }

    static void WriteUfoCoordinateDetails(Coordinates coordinates, string cardinalizedCoordinates, GeoCode geoCode)
    {
        WriteLine($"""
            📌 UFO last spotted at these coordinates:
              • Lat: {coordinates.Latitude}
              • Lon: {coordinates.Longitude}
    
            🗺️ Coordinates are cardinalized (DMS formatted) as:
              • {cardinalizedCoordinates}
    
            🌐 Geo-code HTTP metadata:
              • {geoCode}

            """);
        WriteHyphens();
    }

    static void WriteUfoTravelAlertDetails(Coordinates coordinates, Coordinates? lastObservedCoordinates)
    {
        if (lastObservedCoordinates is { } previousCoordinates)
        {
            var (_, Kilometers, Miles) = CalculateHaversineDistance(
                    previousCoordinates, coordinates);

            WriteLine($"""
                ⚠️ [ALERT] UFO has travelled over {Miles:#,#} mi ({Kilometers:#,#} km) 
                🚀 That's one fast alien! 👽

                """);
            WriteHyphens();
        }
    }

    static void WriteHyphens() => WriteLine($"{new string('-', WindowWidth)}\n");
}
