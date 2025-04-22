using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace vina.Server.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        public DatabaseController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

#if DEBUG
        private static object lockObject = new object();
        [HttpGet(Name = "Drop")]
        public void DbDrop()
        {
            lock (lockObject)
            {
                Seeder.Instance.DbDrop().GetAwaiter().GetResult();
                return;
            }

        }
#endif

        [HttpGet(Name = "GetMyData")]
        public string GetMyData(string token, string language)
        {
            return "";
        }
        [HttpGet(Name = "ForgetMe")]
        public bool ForgetMe(string token)
        {
            return true;
        }

    }
}
