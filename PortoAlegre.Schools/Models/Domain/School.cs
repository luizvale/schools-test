using System.Text.Json.Serialization;

namespace PortoAlegre.Schools.Models
{
    public class School
    {
        public School(){ }

        [JsonPropertyName("_id")]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cep { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        [JsonPropertyName("url_website")]
        public string? UrlWeb { get; set; }
        public double? Distance { get; set; }
        public double? Duration { get; set; }
    }
}
