namespace PortoAlegre.Schools.Models.Protocols
{
    public partial class SchoolHttpResponse
    {
        public string Help { get; set; }
        public bool Success { get; set; }
        public SchoolResult Result { get; set; }
    }
    public partial class SchoolResult
    {
        public List<School> Records { get; set; }
    }
}
