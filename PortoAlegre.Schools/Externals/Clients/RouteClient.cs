using Microsoft.Extensions.Options;
using PortoAlegre.BingMaps.Config;
using PortoAlegre.Schools.Models.Protocols.Route;
using System.Text.Json;

namespace PortoAlegre.Schools.Externals.Clients
{
    public class RouteClient : IRouteClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly BingMapsClientConfig _clientConfig;
        public RouteClient(IHttpClientFactory httpClientFactory, IOptions<BingMapsClientConfig> clientConfig)
        {
            _httpClientFactory = httpClientFactory;
            _clientConfig = clientConfig.Value;
        }

        public async Task<List<double[]>> GetRoute(double[] coordinates, double[] destiny)
        {

            var url = $"{_clientConfig.RouteEndpoint}" +
                      $"?wp.0={coordinates[0]},{coordinates[1]}&wp.1={destiny[0]},{destiny[1]}&key={_clientConfig.Key}";

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var route = await JsonSerializer.DeserializeAsync<RouteHttpResponse>(contentStream, options);
                var routeCoordinates = route!.ResourceSets.First()
                    .Resources.First()
                    .RouteLegs.First()
                    .ItineraryItems.Select(i =>
                    {
                        return i.ManeuverPoint.Coordinates;
                    }).ToList();
                return routeCoordinates;
            }

            else
            {
                throw new Exception(
                    $"StatusCode: {httpResponseMessage.StatusCode}; " +
                    $"Message: {httpResponseMessage.ReasonPhrase}"
                    );
            }
        }

    }
}
