using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vina.Server.Models;
using DBcs;
using vina.Server.Config;
namespace vina.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoPasswordController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDBcs _dBcs;
        private readonly EmailService _emailService;
        private readonly AppSettings _appSettings;

        public NoPasswordController(
            UserManager<IdentityUser> userManager,
            IDBcs dBcs,
            EmailService emailService,
            AppSettings appSettings)
        {
            _emailService = emailService;
            _userManager = userManager;
            _dBcs = dBcs;
            _appSettings = appSettings;
        }

        [HttpGet("{email:minlength(6):maxlength(150)}")]
        public async Task<ActionResult<String>> Login(string email)
        {
            var accept_lang = this.Request.Headers.AcceptLanguage;
            var lang = "en";
            if(accept_lang.Count > 0)
            {
                lang = accept_lang[0];
            }
           
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
            var mailSubject =  await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
               DBTranslation.SelectKeyLangText, new {key="token_mail_subject", lang=lang});
            var mailBody = "<p>" + (await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
               DBTranslation.SelectKeyLangText, new {key="token_mail_body1", lang=lang}))?.Content+ "</p>";
            mailBody += "<p>" + (await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
               DBTranslation.SelectKeyLangText, new {key="token_mail_body2", lang=lang}))?.Content + "</p>";
            mailBody += "<p>" + (await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
               DBTranslation.SelectKeyLangText, new {key="token_mail_body3", lang=lang}))?.Content + "</p>";
            mailBody += "<p>" + (await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
               DBTranslation.SelectKeyLangText, new {key="token_mail_signature", lang=lang}))?.Content + "</p>";
            await _emailService.SendEmailAsync(_appSettings.EmailSettings.EmailSender, email, mailSubject?.Content??"", mailBody);


            var Token = await _userManager.GenerateUserTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win");
            await _userManager.SetAuthenticationTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win", Token);
            Console.WriteLine(Token);
            //send email with token
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
        /// <summary>
        /// Requesting Zoho authorization code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async  Task RequestZohoAuthorization (string code)
        {
            //GET
            //https://accounts.zoho.com/oauth/v2/auth?{client_id}&response_type==code&redirect_uri={redirect_uri}&scope={scope}&access_type={offline or online}
            return;
        }
        public async Task RequestZohoAuthorizationCallback (string code)
        {
            /*
            Sample response format of the URL in which authorization code is received:

            https://{redirect_uri}?code={authorization_code}&location={domain}&accounts-server={accounts_url}

            Example response URL in which authorization code is received:

            https://zylker.com/redirect?code=1000.*******77&location=us&accounts-server=https%3A%2F%2Faccounts.zoho.com
            */
            return ;
        }
        public async Task ExchangeAuthorizationCodeForAccessToken()
        {
            //POST
            /*
            Sample request format in any API platform:

            https://accounts.zoho.com/oauth/v2/token?code={authorization_code}&grant_type=authorization_code&client_id={client_id}&client_secret={client_secret}&redirect_uri={redirect_uri}&scope={Servicename.Scopename.Operation}

            Example request:

            https://accounts.zoho.com/oauth/v2/token?code=1000.****160&grant_type=authorization_code&client_id=1000.R2Z0W****Q5EN&client_secret=39c***921b&redirect_uri=https://zylker.com/redirect &scope=ZohoMail.accounts.READ
            */
            /*
            Sample Response Format:
            { 
            "access_token": "{access_token}", 
            "refresh_token": "{refresh_token}", 
            "api_domain": "https://www.zohoapis.com", 
            "token_type": "Bearer", 
            "expires_in": 3600
            }

            Example Response:

            {
            "access_token": "1000.24a566***********6d276b472.86a1******883491c79a042af",
            "refresh_token": "1000.f113ece**********82d02fb25e9.cc0**********8c57693baea39f",
            "scope": "ZohoMail.accounts.READ",
            "api_domain": "https://www.zohoapis.com",
            "token_type": "Bearer",
            "expires_in": 3600
            }
            */
        }
        public async Task RenewAccessToken()
        {
        //POST
        //https://accounts.zoho.com/oauth/v2/token?refresh_token={refresh_token}&grant_type=refresh_token&client_id={client_id}&client_secret={client_secret}
        /*
        Sample response format:
        {
        "access_token": "{new_access_token}",
        "expires_in": 3600,
        "api_domain": "https://www.zohoapis.com",
        "token_type": "Bearer"
        }
        */
        }
        
    }
        
    
}
