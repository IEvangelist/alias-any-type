// Ensures that all types within these namespaces are globally available.
global using Alias.AnyType;
global using Alias.AnyType.Extensions;
global using Alias.AnyType.ResponseModels;

// Expose all static members of math.
global using static System.Math;

// Alias a coordinates object.
global using Coordinates = (double Latitude, double Longitude);

// Alias representation of degrees-minutes-second (DMS).
global using DMS = (int Degree, int Minute, double Second);

// Alias representation of various distances in different units of measure.
global using Distance = (double Meters, double Kilometers, double Miles);

// Alias a stream of coordinates represented as an async enumerable.
global using CoordinateStream = System.Collections.Generic.IAsyncEnumerable<
    Alias.AnyType.CoordinateGeoCodePair>;

// Alias the CTS, making it simply "Signal".
global using Signal = System.Threading.CancellationTokenSource;
