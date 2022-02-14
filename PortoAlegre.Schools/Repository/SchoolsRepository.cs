using PortoAlegre.Schools.Models;
using System;
using System.Globalization;

namespace PortoAlegre.Schools.Repository
{
    public class SchoolsRepository : ISchoolsRepository
    {
        public List<School> _schools;
        public SchoolsRepository()
        {
        }
        public async Task<List<School>> GetSchoolsList()
        {
            CultureInfo culture = new("en-US");

            var result = await File.ReadAllLinesAsync("Metadata/cadastro_escolas.csv");
            var csvfile = result
                .Select(line => line.Split(';'))
                .Skip(1)
                .Select(x => new School
                {
                    Nome = x[5].Trim(),
                    Numero = Convert.ToInt32(x[8].Trim(), culture),
                    Bairro = x[9].Trim(),
                    Cep = x[10].Trim(),
                    Logradouro = x[7].Trim(),
                    Latitude = decimal.Parse(x[11].Trim(), culture),
                    Longitude = Convert.ToDecimal(x[12].Trim(), culture),
                    Telefone = x[13].Trim(),
                    Email = x[14].Trim(),
                    UrlWeb = x[15].Trim()
                })
                .ToList();

            _schools = csvfile;

            return csvfile;
        }
    }
}
