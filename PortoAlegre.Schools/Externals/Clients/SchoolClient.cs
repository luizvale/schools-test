using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using PortoAlegre.Schools.Config.Models;
using PortoAlegre.Schools.Externals.Clients.Interfaces;
using PortoAlegre.Schools.Models.Domain;
using PortoAlegre.Schools.Models.Protocols;
using System.Text.Json;

namespace PortoAlegre.Schools.Externals.Clients
{
    public class SchoolClient : ISchoolClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly SchoolClientConfig _clientConfig;
        public SchoolClient(IHttpClientFactory httpClientFactory, IOptions<SchoolClientConfig> clientConfig)
        {
            _httpClientFactory = httpClientFactory;
            _clientConfig = clientConfig.Value;
        }
        public async Task<List<School>> OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _clientConfig.ApiConnection)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.UserAgent, "PortoAlegreSchools" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var SchoolsList = await JsonSerializer.DeserializeAsync<SchoolsHttpResponse>(contentStream, options);

                return SchoolsList!.Result.Records;
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
