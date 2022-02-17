namespace PortoAlegre.Schools.Config.Models
{
    public class BingMapsClientConfig
    {
        public BingMapsClientConfig() { }

        public BingMapsClientConfig(string distanceMatrixKey, string routeKey, string bingMapsKey)
        {
            DistanceMatrixEndpoint = distanceMatrixKey;
            RouteEndpoint = routeKey;
            Key = bingMapsKey;
        }

        public const string Client = "LocalSearchApi";

        public string DistanceMatrixEndpoint { get; set; }
        public string RouteEndpoint { get; set; }
        public string Key { get; set; }
    }
}
