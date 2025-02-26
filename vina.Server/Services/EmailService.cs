using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using vina.Server.Config;
using vina.Server.Models;

namespace vina.Server.Controllers
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        private static readonly HttpClient client = new HttpClient();
        private readonly AppSettingsOptions _appSettings;
        private readonly ILogger<EmailService> _logger;
        private readonly IEmailSender _mailJet;

        private readonly string _connectionString;
        public EmailService(
            ILogger<EmailService> logger,
            IConfiguration config,
            IOptions<AppSettingsOptions> appSettings,
            string connectionString,
            IEmailSender mailJet)
        {
            _mailJet = mailJet;
            _logger = logger;
            _appSettings = appSettings.Value;
            _connectionString = connectionString;
            _config = config;
        }
        public async Task SendEmailAsyncMailJet(
            string toEmail,
            string subject,
            string body
            )
        {
            await _mailJet.SendEmailAsync(toEmail,subject,body);                
        }
        public async Task SendEmailAsync(
            string emailToken,
            string fromEmail,
            string toEmail,
            string subject,
            string body
            )
        {
            try
            {
                string json = $"{{\n" +
                                $"    \"fromAddress\": \"{fromEmail}\",\n" +
                                $"    \"toAddress\": \"{toEmail}\",\n" +
                                $"    \"subject\": \"{subject}\",\n" +
                                $"    \"content\": \"{body}\"\n" +
                                $"}}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Remove("Accept");
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken {emailToken}");
                _logger.LogInformation($"Sending email via {_appSettings.EmailSettings.EmailWebserviceUrl}: {json}");
                var httpResponse = await client.PostAsync(_appSettings.EmailSettings.EmailWebserviceUrl, content);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                if(!httpResponse.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error sending email: {responseString}");
                }
                else
                {
                    var j = JsonDocument.Parse(responseString);
                    var root = j.RootElement;
                    var r = root.GetProperty("status").GetProperty("code").GetInt32();
                    if(r>201)
                        _logger.LogError($"Error sending email: {responseString}");
                    else
                        _logger.LogInformation($"Email sent: {responseString}");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
            }
        }
    }
}
