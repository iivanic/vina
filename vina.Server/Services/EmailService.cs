using System.Text;
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

        private readonly string _connectionString;
        public EmailService(
            ILogger<EmailService> logger,
            IConfiguration config,
            IOptions<AppSettingsOptions> appSettings,
            string connectionString)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _connectionString = connectionString;
            _config = config;
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
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken {emailToken}");
                var httpResponse = await client.PostAsync(_appSettings.EmailSettings.EmailWebserviceUrl, content);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
            }
        }
    }
}
