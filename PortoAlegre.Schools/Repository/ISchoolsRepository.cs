using PortoAlegre.Schools.Models.Domain;

namespace PortoAlegre.Schools.Repository
{
    public interface ISchoolsRepository
    {
        Task<List<School>> GetSchoolsList();

    }
}