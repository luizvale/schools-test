using PortoAlegre.Schools.Models;

namespace PortoAlegre.Schools.Repository
{
    public interface ISchoolsRepository
    {
        Task<List<School>> GetSchoolsList();

    }
}