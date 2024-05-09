internal static partial class Program
{
    readonly static HttpClient s_client = new()
    {
        BaseAddress = new Uri("https://api.bigdatacloud.net/data/reverse-geocode-client")
    };

    readonly static JsonSerializerOptions s_options = new(JsonSerializerDefaults.Web);

    static Task<GeoCode?> GetGeocodeAsync(Coordinates coordinates, CancellationToken token) =>
        s_client.GetFromJsonAsync<GeoCode>(
            requestUri: $"?latitude={coordinates.Latitude}&longitude={coordinates.Longitude}", 
            options: s_options, 
            cancellationToken: token);

    static async CoordinateStream GetCoordinateStreamAsync(
        [EnumeratorCancellation] CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var coordinates = GetRandomCoordinates();

            if (await GetGeocodeAsync(coordinates, token) is not { } geoCode)
            {
                break;
            }

            token.ThrowIfCancellationRequested();

            yield return (coordinates, geoCode);
        }
    }
}
