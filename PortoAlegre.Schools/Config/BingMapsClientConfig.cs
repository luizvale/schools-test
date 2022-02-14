namespace PortoAlegre.BingMaps.Config
{
    public class BingMapsClientConfig
    {
        public BingMapsClientConfig() { }
        public const string Client = "LocalSearchApi";
        public string DistanceMatrixEndpoint { get; set; }
        public string RouteEndpoint { get; set; }
        public string Key { get; set; }
    }
}
