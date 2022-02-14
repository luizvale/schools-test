using PortoAlegre.Schools.Models;

namespace PortoAlegre.Schools.Externals.Clients.Interfaces
{
    public interface ISchoolClient
    {
        Task<List<School>> OnGet(string sql);
    }
}
