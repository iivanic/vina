using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        public async Task<string> DbSeed()
        {
            lock (lockObject)
            {
                return Seeder.Instance.DbSeed().GetAwaiter().GetResult();
            }
        
        }
        [HttpGet(Name = "Drop")]
        public async Task DbDrop()
        {
            lock (lockObject)
            {
                Seeder.Instance.DbDrop().GetAwaiter().GetResult();
                return ;
            }
        
        }
#endif
        [HttpGet(Name = "GetMyOptions")]
        public async Task<SortedList<string,string>> GetMyOptions(string token, string language)
        {
            var ret =  new SortedList<string, string>();

            if (await Seeder.Instance.DbExists())
            {
#if DEBUG
                ret.Add("Drop Database", "auth/dbdrop");
#endif
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
