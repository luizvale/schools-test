using PortoAlegre.Schools.Models;

namespace PortoAlegre.Schools.Externals.Clients.Interfaces
{
    public interface IBingMapsClient
    {
        public Task<List<Models.Protocols.BingMaps.BingMapsResult>> GetDistanceMatrix(MatrixDistance matrixDistance);
    }
}
