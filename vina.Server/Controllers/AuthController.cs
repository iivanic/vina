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
        [HttpGet(Name = "GetMyOptions")]
        public SortedList<string,string> GetMyOptions(string token, string language)
        {
            var ret =  new SortedList<string, string>();
#if DEBUG
            if(Seeder.Instance.DbExists())
            {
                ret.Add("Drop Database", "auth/dbdrop");
                //isAdmin
                ret.Add("Users", "auth/getusers");
                ret.Add("Orders", "auth/getorders");
                ret.Add("DeleteUser", "auth/deleteuser");
                ret.Add("DeleteOrder", "auth/deleteorder");
                ret.Add("SetOrderStatus", "auth/setorderstatus");
            }
            else
            {
                ret.Add("Create Database", "auth/dbseed");
            }
#endif
            return ret;
        }


        [HttpGet(Name = "GetToken")]
        public WeatherForecast GetToken(string token)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Freezing"
            };
        }
        [HttpGet(Name = "GetMyData")]
        public WeatherForecast GetMyData(string token, string language)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Freezing"
            };
        }
        [HttpGet(Name = "ForgetMe")]
        public WeatherForecast ForgetMe(string token)
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
