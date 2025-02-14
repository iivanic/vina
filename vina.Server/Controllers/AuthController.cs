using Microsoft.AspNetCore.Mvc;

namespace vina.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

#if DEBUG
        private static object lockObject = new object();
        [HttpGet(Name = "Seed")]
        public string DbSeed()
        {
            lock (lockObject)
            {
                return Seeder.Instance.DbSeed();
            }
        
        }
        [HttpGet(Name = "Drop")]
        public string DbDrop()
        {
            lock (lockObject)
            {
                return Seeder.Instance.DbDrop();
            }
        
        }
#endif
        [HttpGet(Name = "GetToken")]
        public WeatherForecast GetToken(WeatherForecast token)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Freezing"
            };
        }

    }
}
