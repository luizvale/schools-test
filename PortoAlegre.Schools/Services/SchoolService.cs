using PortoAlegre.Schools.Models;
using PortoAlegre.Schools.Services.Interfaces;
using PortoAlegre.Schools.Repository;

namespace PortoAlegre.Schools.Services
{
    public class SchoolService : ISchoolService
    {
        public readonly ISchoolsRepository SchoolRepository;
        public SchoolService(ISchoolsRepository schoolRepository)
        {
            SchoolRepository = schoolRepository;
        }

        public async Task<List<School>> GetList()
        {
           var listSchools = await SchoolRepository.GetSchoolsList();
            return listSchools;
        }
    }
}
