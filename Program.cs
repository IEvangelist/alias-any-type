using Signal signal = new();

// Change this delay to see different UFOs sighting intervals.
const int UfoSightingInterval = 7_500;

OutputEncoding = Encoding.UTF8;
CancelKeyPress += (sender, e) =>
{
    signal.Cancel();
    e.Cancel = true; // Prevent the app from closing immediately
};

WriteIntroduction();

try
{
    Coordinates? lastObservedCoordinates = null;

    await foreach (var coordinate
        in GetCoordinateStreamAsync(signal.Token))
    {
        (Coordinates coordinates, GeoCode geoCode) = coordinate;

        // Use extension method, that extends the aliased type.
        var cardinalizedCoordinates = coordinates.ToCardinalizedString();

        // Write UFO coordinate details to the console.
        WriteUfoCoordinateDetails(coordinates, cardinalizedCoordinates, geoCode);

        // Write travel alert, including distance travelled.
        WriteUfoTravelAlertDetails(coordinates, lastObservedCoordinates);

        await Task.Delay(UfoSightingInterval, signal.Token);

        lastObservedCoordinates = coordinates;
    }
}
catch (Exception ex)
{
    _ = ex;
}