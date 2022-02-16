using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjektAPI.Model;
using ProjektAPI.Services;

namespace ProjektAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherController(ILogger<WeatherController> logger, IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        [HttpGet]
        [AllowAnonymous]
        //public IEnumerable<Weather> GetAll()        
        public ActionResult<IEnumerable<Weather>> GetAll()
        {
            var temp = _weatherService.GetAll();
            return Ok(temp);
        }
        [HttpGet("{id}")]
        public ActionResult<Weather> Get(int id)
        {
            return Ok(_weatherService.Get(id));
        }
        [HttpGet("Date{date}")]
        public ActionResult<IEnumerable<Weather>> Get(DateTime date)
        {
            return Ok(_weatherService.GetByDate(date));
        }
        [HttpGet("Date{from}/{to}")]
        public ActionResult<IEnumerable<Weather>> Get(DateTime from, DateTime to)
        {
            return Ok(_weatherService.GetBetweenDate(from,to));
        }
        [HttpGet("Temp{from}/{to}")]
        public ActionResult<IEnumerable<Weather>> Get(float from, float to)
        {
            return Ok(_weatherService.GetBetweenTemperature(from, to));
        }
        [HttpPost]
        public void Post([FromBody]NewWeather newWeather)
        {
            _weatherService.Post(newWeather);
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]NewWeather newWeather )
        {
            _weatherService.Put(id,newWeather);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _weatherService.Delete(id);
        }
    }
}