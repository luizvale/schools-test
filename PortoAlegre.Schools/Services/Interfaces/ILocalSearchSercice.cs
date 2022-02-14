using PortoAlegre.Schools.Models;
using PortoAlegre.Schools.Models.Domain;

namespace PortoAlegre.Schools.Services.Interfaces
{
    public interface ILocalSearchSercice
    {
        Task<List<School>> ListSchoolsByDistance(Address address);
        Task<List<double[]>> GetRoute(double[] coordinates, double[] destiny);
    }
}
