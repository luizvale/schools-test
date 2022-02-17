using Microsoft.AspNetCore.Mvc;
using PortoAlegre.Schools.Models.Domain;
using PortoAlegre.Schools.Services.Interfaces;

namespace PortoAlegre.Schools.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ControllerBase
    {
        public readonly ISchoolService SchoolService;
        public readonly ILocalSearchSercice LocalSearchService;

        public SchoolsController(ISchoolService schoolService, ILocalSearchSercice localsearchService)
        {
            SchoolService = schoolService;
            LocalSearchService = localsearchService;
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(School))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var listSchools = await SchoolService.GetList();

            if (listSchools == null)
                return NotFound();

            return StatusCode(200, listSchools);
        }

        [HttpPost("ordered")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(School))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListSchoolsByDistance([FromBody] Address address)
        {
            if (address.Nome == null
                || address.Cep == null
                || address.Bairro == null
                )
                return StatusCode(400);

            var schools = await LocalSearchService.ListSchoolsByDistance(address);

            if (schools is null)
                NotFound();

            return StatusCode(200, schools);
        }

        [HttpPost("route")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<double[]>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRouteBetweenSchools([FromBody] Itinerary itinerary)
        {
            if (itinerary.Origin is null || itinerary.Destiny is null)
                return StatusCode(400);

            var route = await LocalSearchService.GetRoute(itinerary.Origin, itinerary.Destiny);

            if(route == null)
                return NotFound();

            return StatusCode(200, route);
        }



        [HttpPost("Ping")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<double[]>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Ping([FromBody] string ping)
        {
            return StatusCode(200, "pong !");
        }

        [HttpPost("map")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<double[]>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMapRoute([FromBody] Itinerary itinerary)
        {
            if (itinerary.Origin is null || itinerary.Destiny is null)
                return StatusCode(400);

            var map = await LocalSearchService.GetMap(itinerary.Origin, itinerary.Destiny);
            var mapImg = File(map, "image/jpeg");
            if (map == null)
                return NotFound();

            return StatusCode(200, mapImg);
        }

    }

    public class Itinerary
    {
        public double[] Origin { get; set; }
        public double[] Destiny { get; set; }
    }
}
