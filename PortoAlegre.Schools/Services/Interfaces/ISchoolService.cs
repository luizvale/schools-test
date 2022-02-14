using PortoAlegre.Schools.Models;

namespace PortoAlegre.Schools.Services.Interfaces
{
    public interface ISchoolService
    {
        Task<List<School>> GetList();
    }
}
