namespace PortoAlegre.Schools.Externals.Clients.Interfaces
{
    public interface IRouteClient
    {
        Task<List<double[]>> GetRoute(double[] coordinates, double[] destiny);
        Task<byte[]> ReturnMap(double[] coordinates, double[] destiny);
    }
}