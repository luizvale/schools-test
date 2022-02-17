using PortoAlegre.Schools.Models.Domain;
using PortoAlegre.Schools.Models.Protocols;

namespace PortoAlegre.Schools.Externals.Clients.Interfaces
{
    public interface IBingMapsClient
    {
        public Task<List<BingMapsResult>> GetDistanceMatrix(MatrixDistance matrixDistance);
    }
}
