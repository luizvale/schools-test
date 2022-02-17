using PortoAlegre.Schools.Models.Domain;

namespace PortoAlegre.Schools.Externals.Clients.Interfaces
{
    public interface ISchoolClient
    {
        Task<List<School>> OnGet();
    }
}
