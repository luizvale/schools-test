using PortoAlegre.Schools.Services.Interfaces;
using PortoAlegre.Schools.Repository;
using PortoAlegre.Schools.Externals.Clients.Interfaces;
using PortoAlegre.Schools.Models.Domain;

namespace PortoAlegre.Schools.Services
{
    public class SchoolService : ISchoolService
    {
        public readonly ISchoolsRepository SchoolRepository;
        public readonly ISchoolClient SchoolClient;
        public SchoolService(ISchoolClient schoolClient)
        {
            SchoolClient = schoolClient;
        }

        public async Task<List<School>> GetList()
        {
           var listSchools = await SchoolClient.OnGet();
            return listSchools;
        }
    }
}
