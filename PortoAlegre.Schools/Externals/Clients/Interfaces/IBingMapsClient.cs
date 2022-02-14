using PortoAlegre.Schools.Models;

namespace PortoAlegre.Schools.Externals.Clients.Interfaces
{
    public interface IBingMapsClient
    {
        public Task<List<Models.Protocols.BingMapsResult>> GetDistanceMatrix(MatrixDistance matrixDistance);
        Task<List<double[]>> GetRoute(double[] coordinates, double[] destiny);
    }
}
