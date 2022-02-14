namespace PortoAlegre.Schools.Models
{
    public class MatrixDistance
    {
        public string TravelMode { get; set; }
        public Coordinates[] Origins { get; set; }
        public Coordinates[] Destinations { get; set; }

        public MatrixDistance(Coordinates[] _origins, Coordinates[] _destinations, string _travelMode)
        {
            Origins = _origins;
            Destinations = _destinations;
            TravelMode = _travelMode;
        }
    }
}
