using PortoAlegre.Schools.Services.Interfaces;
using PortoAlegre.Schools.Models.Domain;
using PortoAlegre.Schools.Externals.Clients.Interfaces;

namespace PortoAlegre.Schools.Services
{
    public class LocalSearchService : ILocalSearchSercice
    {
        public readonly IRouteClient RouteClient;
        public readonly IBingMapsClient BingMapsClient;
        public readonly ISchoolService SchoolService;
        public LocalSearchService(IRouteClient routeClient, IBingMapsClient bingMapsClient, ISchoolService schoolService)
        {
            RouteClient = routeClient;
            BingMapsClient = bingMapsClient;
            SchoolService = schoolService;
        }
        public async Task<List<School>> ListSchoolsByDistance(Address address)
        {

            var listSchools = await SchoolService.GetList();

            var schoolOrigin = listSchools.FirstOrDefault(s => (
                s.Nome == address.Nome &&
                s.Bairro == address.Bairro &&
                s.Numero == address.Numero &&
                s.Cep == address.Cep
            ));

            var destinations = listSchools.Select(s => new Coordinates { 
                latitude = s.Latitude, 
                longitude = s.Longitude 
            }).ToArray();

            var matrixDistance = new MatrixDistance(
                _origins: new Coordinates[] { 
                    new Coordinates { 
                        latitude = schoolOrigin!.Latitude, 
                        longitude = schoolOrigin.Longitude
                    } 
                },
                _destinations: destinations,
                _travelMode: "driving"
                );

            var Distances = await BingMapsClient.GetDistanceMatrix(matrixDistance);

            var listSchoolsCalculated = listSchools.Select((s, index) =>
            {
                s.Distance = Distances.FirstOrDefault(s => s.DestinationIndex == index)!.TravelDistance;
                s.Duration = Distances.FirstOrDefault(s => s.DestinationIndex == index)!.TravelDuration;

                return s;
            }).OrderBy(s => s.Distance);

            return listSchoolsCalculated.ToList();
        }

        public async Task<List<double[]>> GetRoute(double[] coordinates, double[] destiny)
        {
            var Distances = await RouteClient.GetRoute(coordinates, destiny);

            return Distances;
        }

        public async Task<byte[]> GetMap(double[] destiny, double[] origin)
        {
            byte[] map = await RouteClient.ReturnMap(destiny, origin);
            return map;
        }
    }
}
