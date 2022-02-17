using Microsoft.Extensions.Options;
using PortoAlegre.Schools.Config.Models;
using PortoAlegre.Schools.Externals.Clients.Interfaces;
using PortoAlegre.Schools.Models.Domain;
using PortoAlegre.Schools.Models.Protocols;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PortoAlegre.Schools.Externals.Clients
{
    public class BingMapsClient : IBingMapsClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly BingMapsClientConfig _clientConfig;
        public BingMapsClient(IHttpClientFactory httpClientFactory, IOptions<BingMapsClientConfig> clientConfig)
        {
            _httpClientFactory = httpClientFactory;
            _clientConfig = clientConfig.Value;
        }

        public async Task<List<BingMapsResult>> GetDistanceMatrix(MatrixDistance matrixDistance)
        {
            var matrixSerialized = JsonSerializer.Serialize(matrixDistance);

            var url = $"{_clientConfig.DistanceMatrixEndpoint}?key={_clientConfig.Key}";

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(matrixSerialized, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpClient = _httpClientFactory.CreateClient();

            var httpResponseMessage = await httpClient.SendAsync(request);

            httpResponseMessage.EnsureSuccessStatusCode();

            if (httpResponseMessage.IsSuccessStatusCode)
            {

                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var SchoolsList = await JsonSerializer.DeserializeAsync<BingMapsHttpResponse>(contentStream);

                return SchoolsList!.resourceSets.FirstOrDefault()!.resources.FirstOrDefault()!.Results;
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
