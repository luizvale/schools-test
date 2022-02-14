using System.Text.Json.Serialization;

namespace PortoAlegre.Schools.Models.Protocols
{
    public partial class RouteHttpResponse
    {
        public List<ResourceSet> ResourceSets { get; set; }
        public long StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string TraceId { get; set; }
    }

    public partial class ResourceSet
    {
        public List<Resource> Resources { get; set; }
    }

    public partial class Resource
    {
        public string DurationUnit { get; set; }
        public List<RouteLeg> RouteLegs { get; set; }
        public double TravelDistance { get; set; }
        public long TravelDuration { get; set; }
    }

    public partial class RouteLeg
    {
        public ActualEnd ActualEnd { get; set; }
        public ActualEnd ActualStart { get; set; }
        public string Description { get; set; }
        public List<ItineraryItem> ItineraryItems { get; set; }
    }

    public partial class ActualEnd
    {
        public double[] Coordinates { get; set; }
    }

    public partial class ItineraryItem
    {
        public ActualEnd ManeuverPoint { get; set; }
        public long TravelDuration { get; set; }
    }
}
