using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vina.Server.Models;
using DBcs;
using vina.Server.Config;
using Microsoft.AspNetCore.Http.Extensions;
using System.Web;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace vina.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoPasswordController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDBcs _dBcs;
        private readonly EmailService _emailService;
        private readonly AppSettingsOptions _appSettings;
        private readonly ILogger<ProductsController> _logger;

        public NoPasswordController(
            ILogger<ProductsController> logger,
            UserManager<IdentityUser> userManager,
            IDBcs dBcs,
            EmailService emailService,
            IOptions<AppSettingsOptions> appSettings)
        {
            _logger = logger;
            _emailService = emailService;
            _userManager = userManager;
            _dBcs = dBcs;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{email:minlength(6):maxlength(150)}")]
        public async Task<ActionResult<String>> Login(string email)
        {
            var accept_lang = this.Request.Headers.AcceptLanguage;
            var lang = "en";
            if (accept_lang.Count > 0)
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
            var Token = await _userManager.GenerateUserTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win");
            await _userManager.SetAuthenticationTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win", Token);

            var mailSubject = await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
                DBTranslation.SelectKeyLangText, new { key = "token_mail_subject", lang = lang });
            var mailBody = "<p>" + (await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
                DBTranslation.SelectKeyLangText, new { key = "token_mail_body1", lang = lang }))?.Content + "</p>";
            mailBody += "<p>" + (await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
                DBTranslation.SelectKeyLangText, new { key = "token_mail_body2", lang = lang }))?.Content + "</p>";
            mailBody += $"<p><a rel='notrack' href='{Request.Scheme}://{Request.Host}/nopassword/{HttpUtility.UrlEncode(Token)}/{email}'>{(await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
                DBTranslation.SelectKeyLangText, new { key = "token_mail_click_here", lang = lang }))?.Content}</a></p>";

            mailBody += "<p>" + (await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
                DBTranslation.SelectKeyLangText, new { key = "token_mail_body3", lang = lang }))?.Content + "</p>";
            mailBody += "<p>" + (await _dBcs.RunQuerySingleOrDefaultAsync<DBTranslation>(
                DBTranslation.SelectKeyLangText, new { key = "token_mail_signature", lang = lang }))?.Content + "</p>";


            //send email with mailjet
            // await _emailService.SendEmailAsyncMailJet(email, mailSubject?.Content ?? "", mailBody);
            // return  NoContent();

            var zoho_email = await _dBcs.RunQuerySingleOrDefaultAsync<DBZohoMail>(DBZohoMail.SelectSingleText, 1);
            if (zoho_email == null)
            {
#if DEBUG
                // we need to RequestZohoAuthorization 
                await RequestZohoAuthorization();
                // waiting for callback, nothing to do...
                return Ok();
#else
                _logger.LogError("Zoho email settings not found, use DEBUG build to request Zoho authorization");
                return BadRequest();
#endif
            }
             if (zoho_email.AccessTokenValidUntil < DateTime.UtcNow || string.IsNullOrEmpty(zoho_email.AccessToken))
             {
                 if (string.IsNullOrEmpty(zoho_email.RefreshToken))
                 {
                     // we need to RequestZohoAuthorization 
                     await ExchangeAuthorizationCodeForAccessToken(zoho_email);
                 }
                 else
                 {
                    await RenewAccessToken(zoho_email);
                 }
             }
            if (zoho_email.AccessToken == null)
            {
                _logger.LogError("Zoho access token not found");
                return BadRequest();
            }
            await _emailService.SendEmailAsync1(
                zoho_email.AccessToken,
                _appSettings.EmailSettings.EmailSender, email, mailSubject?.Content ?? "", mailBody);

            //send email with token
            return NoContent();
        }

        [HttpGet("{token:minlength(6):maxlength(2500)}/{email:minlength(6):maxlength(150)}")]
        public async Task<ActionResult<String>> Verify(string token, string email)
        {
            // Fetch your user from the database
            var User = await _userManager.FindByEmailAsync(email);
            if (User == null)
            {
                return Unauthorized();
            }

            var IsValid = await _userManager.VerifyUserTokenAsync(User, "NPTokenProvider", "nopassword-for-the-win", token);
            if (IsValid)
            {
                // TODO: Generate a bearer token
                var BearerToken = Guid.NewGuid().ToString("N");
                return BearerToken;
            }
            return Unauthorized();
        }

#if DEBUG
        /// <summary>
        /// Requesting Zoho authorization code
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> RequestZohoAuthorization()
        {
            //GET
            //https://accounts.zoho.com/oauth/v2/auth?{client_id}&response_type==code&redirect_uri={redirect_uri}&scope={scope}&access_type={offline or online}
            using (var client = new HttpClient())
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["client_id"] = _appSettings.EmailSettings.ClientId;
                query["response_type"] = "code";
                query["redirect_uri"] = _appSettings.EmailSettings.RedirectUri;
                query["scope"] = "ZohoMail.messages.ALL";
                query["access_type"] = "offline";

                var uriBuilder = new UriBuilder("https://accounts.zoho.eu/oauth/v2/auth")
                {
                    Query = query.ToString()
                };

                var response = await client.GetAsync(uriBuilder.Uri);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    _logger.LogInformation($"Open link to authenticate: {response.RequestMessage?.RequestUri}");
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }

        }
        /// <summary>
        /// Zoho authorization callback
        /// </summary>
        /// <param name="code"></param>
        /// <param name="location"></param>
        /// <param name="accounts_server"></param>
        /// <returns></returns>
        [HttpGet("zohoredirect")]
        public async Task RequestZohoAuthorizationCallback([FromQuery] string code, [FromQuery] string location, [FromQuery(Name = "accounts-server")] string accounts_server)
        {
            _logger.LogInformation($"RequestZohoAuthorizationCallback: code: {code}, location: {location}, accounts_server: {accounts_server}");
            /*
            Sample response format of the URL in which authorization code is received:

            https://{redirect_uri}?code={authorization_code}&location={domain}&accounts-server={accounts_url}

            Example response URL in which authorization code is received:

            https://zylker.com/redirect?code=1000.*******77&location=us&accounts-server=https%3A%2F%2Faccounts.zoho.com
            */
            var zoho_email = await _dBcs.RunQuerySingleOrDefaultAsync<DBZohoMail>(DBZohoMail.SelectSingleText, 1);
            if (zoho_email == null)
            {
                zoho_email = new DBZohoMail();
                zoho_email.Id = 1;
                zoho_email.AuthorizationCode = code;
                zoho_email.AuthorizationLocation = location;
                zoho_email.AuthorizationAccountsServer = accounts_server;
                zoho_email.AuthorizationCodeTimestamp = DateTime.UtcNow;
                zoho_email.AccessTokenExpiresIn = 0;
                zoho_email.AccessTokenTimestamp = DateTime.MinValue;
                zoho_email.AccessTokenValidUntil = DateTime.MinValue;

                await _dBcs.RunNonQueryAsync(DBZohoMail.InsertText, zoho_email);
            }
            else
            {
                zoho_email.AuthorizationLocation = location;
                zoho_email.AuthorizationAccountsServer = accounts_server;
                zoho_email.AuthorizationCode = code;
                zoho_email.AuthorizationCodeTimestamp = DateTime.UtcNow;
                zoho_email.AccessTokenExpiresIn = 0;
                zoho_email.AccessTokenTimestamp = DateTime.MinValue;
                zoho_email.AccessTokenValidUntil = DateTime.MinValue;

                await _dBcs.RunNonQueryAsync(DBZohoMail.UpdateText, zoho_email);
            }
            //  await  ExchangeAuthorizationCodeForAccessToken(zoho_email);

            return;
        }
#endif       
        public async Task<ActionResult> ExchangeAuthorizationCodeForAccessToken(DBZohoMail zoho_email)
        {
            //POST
            /*
            Sample request format in any API platform:

            https://accounts.zoho.com/oauth/v2/token?code={authorization_code}&grant_type=authorization_code&client_id={client_id}&client_secret={client_secret}&redirect_uri={redirect_uri}&scope={Servicename.Scopename.Operation}

            Example request:

            https://accounts.zoho.com/oauth/v2/token?code=1000.****160&grant_type=authorization_code&client_id=1000.R2Z0W****Q5EN&client_secret=39c***921b&redirect_uri=https://zylker.com/redirect &scope=ZohoMail.accounts.READ
            */
            using (var client = new HttpClient())
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["code"] = zoho_email.AuthorizationCode;
                query["grant_type"] = "authorization_code";
                query["client_id"] = _appSettings.EmailSettings.ClientId;
                query["client_secret"] = _appSettings.EmailSettings.ClientSecret;
                query["redirect_uri"] = _appSettings.EmailSettings.RedirectUri;
                query["scope"] = "ZohoMail.messages.ALL";

                var uriBuilder = new UriBuilder($"https://accounts.zoho.eu/oauth/v2/token")
                {
                    Query = query.ToString()
                };
                HttpContent content = new StringContent("");
                _logger.LogInformation($"ExchangeAuthorizationCodeForAccessToken: {uriBuilder.Uri}");
                var response = await client.PostAsync(uriBuilder.Uri, content);


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
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var j = JsonDocument.Parse(responseBody);
                    var root = j.RootElement;
                    zoho_email.AccessToken = root.GetProperty("access_token").GetString();
                    if (string.IsNullOrEmpty(zoho_email.AccessToken))
                        _logger.LogError("Zoho Access Token not recieved");

                    zoho_email.ApiDomain = root.GetProperty("api_domain").GetString();

                    if (root.TryGetProperty("refresh_token", out var rt))
                        zoho_email.RefreshToken = rt.GetString();
                    else
                        _logger.LogWarning("Zoho Refresh Token not recieved.");

                    zoho_email.AccessTokenExpiresIn = root.GetProperty("expires_in").GetInt32();
                    zoho_email.AccessTokenType = root.GetProperty("token_type").GetString();
                    zoho_email.AccessTokenTimestamp = DateTime.UtcNow;
                    zoho_email.AccessTokenValidUntil = zoho_email.AccessTokenTimestamp.Value.AddSeconds(
                        Convert.ToDouble(zoho_email.AccessTokenExpiresIn));
                    await _dBcs.RunNonQueryAsync(DBZohoMail.UpdateText, zoho_email);
                    _logger.LogInformation($"ExchangeAuthorizationCodeForAccessToken: Zoho email settings updated: {responseBody}");
                    return Ok(responseBody);
                }
                else
                {

                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        public async Task<ActionResult> RenewAccessToken(DBZohoMail zoho_email)
        {
            //POST
            //https://accounts.zoho.com/oauth/v2/token?refresh_token={refresh_token}&grant_type=refresh_token&client_id={client_id}&client_secret={client_secret}


            using (var client = new HttpClient())
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["refresh_token"] = zoho_email.RefreshToken;
                query["grant_type"] = "refresh_token";
                query["client_id"] = _appSettings.EmailSettings.ClientId;
                query["client_secret"] = _appSettings.EmailSettings.ClientSecret;

                var uriBuilder = new UriBuilder($"{zoho_email.AuthorizationAccountsServer}/oauth/v2/token")
                {
                    Query = query.ToString()
                };
                HttpContent content = new StringContent("");
                _logger.LogInformation($"RenewAccessToken: {uriBuilder.Uri}");
                var response = await client.PostAsync(uriBuilder.Uri, content);


                /*
            Sample response format:
            {
            "access_token": "{new_access_token}",
            "expires_in": 3600,
            "api_domain": "https://www.zohoapis.com",
            "token_type": "Bearer"
            }
            */
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var j = JsonDocument.Parse(responseBody);
                    var root = j.RootElement;
                    zoho_email.AccessToken = root.GetProperty("access_token").GetString();
                    zoho_email.AccessTokenExpiresIn = root.GetProperty("expires_in").GetInt32();
                    zoho_email.AccessTokenTimestamp = DateTime.UtcNow;
                    zoho_email.AccessTokenValidUntil = zoho_email.AccessTokenTimestamp.Value.AddSeconds(
                        Convert.ToDouble(zoho_email.AccessTokenExpiresIn));
                    await _dBcs.RunNonQueryAsync(DBZohoMail.UpdateText, zoho_email);
                    _logger.LogInformation($"RenewAccessToken: Zoho email settings updated: {responseBody}");
                    return Ok(responseBody);
                }
                else
                {

                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }


        }

    }


}
