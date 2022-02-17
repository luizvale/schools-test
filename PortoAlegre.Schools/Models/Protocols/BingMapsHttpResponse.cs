using System.Text.Json.Serialization;

namespace PortoAlegre.Schools.Models.Protocols
{
    public partial class BingMapsHttpResponse
    {
        [JsonPropertyName("resourceSets")]
        public ResourceSet[] resourceSets { get; set; }

        [JsonPropertyName("statusDescription")]
        public string StatusDescription { get; set; }

        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }
    }

    public partial class ResourceSet
    {
        [JsonPropertyName("resources")]
        public Resource[] resources { get; set; }
    }

    public partial class Resource
    {
        [JsonPropertyName("destinations")]
        public Destination[] Destinations { get; set; }

        [JsonPropertyName("origins")]
        public Destination[] Origins { get; set; }

        [JsonPropertyName("results")]
        public List<BingMapsResult> Results { get; set; }
    }

    public partial class Destination
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

    public partial class BingMapsResult
    {
        [JsonPropertyName("destinationIndex")]
        public long DestinationIndex { get; set; }

        [JsonPropertyName("originIndex")]
        public long OriginIndex { get; set; }

        [JsonPropertyName("totalWalkDuration")]
        public long TotalWalkDuration { get; set; }

        [JsonPropertyName("travelDistance")]
        public double TravelDistance { get; set; }

        [JsonPropertyName("travelDuration")]
        public double TravelDuration { get; set; }
    }
}
