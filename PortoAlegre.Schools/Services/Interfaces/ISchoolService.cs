using PortoAlegre.Schools.Models.Domain;

namespace PortoAlegre.Schools.Services.Interfaces
{
    public interface ISchoolService
    {
        Task<List<School>> GetList();
    }
}
