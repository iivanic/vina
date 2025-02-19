using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace vina.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoPasswordController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public NoPasswordController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{email:minlength(6):maxlength(150)}")]
        public async Task<ActionResult<String>> Login(string email)
        {
            // Create or Fetch your user from the database
            var User = await _userManager.FindByEmailAsync(email);
            if (User == null)
            {
                User = new IdentityUser();
                User.Email = email;
                User.UserName = email;
                var IdentityResult = await _userManager.CreateAsync(User);
                if (IdentityResult.Succeeded == false)
                {
                    return BadRequest();
                }
            }

            var Token = await _userManager.GenerateUserTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win");
            
            Console.WriteLine(Token);
            return NoContent();
        }

    [HttpGet("{token:alpha:minlength(6):maxlength(50)}/{email:minlength(6):maxlength(150)}")]
        public async Task<ActionResult<String>> Verify(string Token, string Email)
        {
            // Fetch your user from the database
            var User = await _userManager.FindByEmailAsync(Email);
            if (User == null)
            {
                return NotFound();
            }

            var IsValid = await _userManager.VerifyUserTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win", Token);
            if (IsValid)
            {
                // TODO: Generate a bearer token
                var BearerToken = Guid.NewGuid().ToString("N");
                return BearerToken;
            }
            return Unauthorized();
        }
    }
}
