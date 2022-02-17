using PortoAlegre.Schools.Models.Domain;

namespace PortoAlegre.Schools.Models.Protocols

{
    public partial class SchoolsHttpResponse
    {
        public Uri Help { get; set; }

        public bool Success { get; set; }

        public Result Result { get; set; }
    }

    public partial class Result
    {
        public List<School> Records { get; set; }
    }

}