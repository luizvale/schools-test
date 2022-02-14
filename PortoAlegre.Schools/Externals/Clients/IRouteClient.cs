namespace PortoAlegre.Schools.Externals.Clients
{
    public interface IRouteClient
    {
        Task<List<double[]>> GetRoute(double[] coordinates, double[] destiny);

    }
}