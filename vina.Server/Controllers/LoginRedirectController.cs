﻿using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using vina.Server.Models;

namespace vina.Server.Controllers;

[Route("[controller]")]
public class LoginRedirectController : Controller
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _iconfiguration;

    public LoginRedirectController(UserManager<IdentityUser> userManager, IConfiguration iconfiguration)
    {
        _userManager = userManager;
        _iconfiguration = iconfiguration;
    }


    [HttpGet]
    public async Task<IActionResult> Login(string token, string email, string returnUrl)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if(user is null)
        {
            return Unauthorized();
        }
        var isValid = await _userManager.VerifyUserTokenAsync(user, "Default", "passwordless-auth", token);

        if (isValid)
        {
            await _userManager.UpdateSecurityStampAsync(user);

            await HttpContext.SignInAsync(
                IdentityConstants.ApplicationScheme,
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new List<Claim>
                        {
                            new Claim("sub", user.Id)
                        },
                        IdentityConstants.ApplicationScheme
                    )
                )
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = _iconfiguration["JWT:Key"];
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT:Key configuration value is missing.");
            }
            var tokenKey = Encoding.UTF8.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwToken = tokenHandler.CreateToken(tokenDescriptor);

            if(returnUrl is null)
            {
                return Ok(new Tokens 
                { 
                    Token = tokenHandler.WriteToken(jwToken) 
                });
            }

            return new RedirectResult($"~{returnUrl}");
        }

        return Unauthorized();
    }
}
