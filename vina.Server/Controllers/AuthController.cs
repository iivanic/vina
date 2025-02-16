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
        private readonly AuthService _authService;

        public AuthController(ILogger<AuthController> logger, AuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpGet("{lang:alpha:minlength(2):maxlength(2)}/token/{token:alpha:minlength(6):maxlength(50)}")]
        public async Task<SortedList<string, string>> MyOptions(string lang, string token)
        {
            var ret = new SortedList<string, string>();

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



        [HttpGet("{lang:alpha:minlength(2):maxlength(2)}/email/{email:alpha:minlength(6):maxlength(50)}")]
        public string Token(string lang, string email)
        {
            return "";
        }

    }
}
