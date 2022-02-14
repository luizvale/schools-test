namespace PortoAlegre.Schools.Externals.Clients.Interfaces
{
    public interface IRouteClient
    {
        Task<List<double[]>> GetRoute(double[] coordinates, double[] destiny);

    }
}