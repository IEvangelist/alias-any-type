internal static partial class Program
{
    readonly static HttpClient s_client = new()
    {
        BaseAddress = new Uri("https://api.bigdatacloud.net/data/reverse-geocode-client")
    };

    readonly static JsonSerializerOptions s_options = new(JsonSerializerDefaults.Web);

    // For example, Microsoft Campus has the following coordinates:
    //   https://api.bigdatacloud.net/data/reverse-geocode-client?latitude=47.6370&longitude=-122.1240
    static Task<GeoCode?> GetGeocodeAsync(Coordinates coordinates, CancellationToken token) =>
        s_client.GetFromJsonAsync<GeoCode>(
            requestUri: $"?latitude={coordinates.Latitude}&longitude={coordinates.Longitude}", 
            options: s_options, 
            cancellationToken: token);

    static async CoordinateStream GetCoordinateStreamAsync(
        [AsyncCancelable] CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        do
        {
            var coordinates = GetRandomCoordinates();

            if (await GetGeocodeAsync(coordinates, token) is not { } geoCode)
            {
                break;
            }

            token.ThrowIfCancellationRequested();

            yield return new CoordinateGeoCodePair(
                Coordinates: coordinates, 
                GeoCode: geoCode);
        }
        while (!token.IsCancellationRequested);
    }
}
