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
        private readonly AppSettings _appSettings;
        private readonly ILogger<EmailService> _logger;

        private readonly string _connectionString;
        public EmailService(
            ILogger<EmailService> logger,
            IConfiguration config,
            IOptions<AppSettings> appSettings,
            string connectionString)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _connectionString = connectionString;
            _config = config;
        }

        public async Task SendEmailAsync(
            string fromEmail,
            string toEmail,
            string subject,
            string body)
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
                content.Headers.Add("Authorization", $"{_appSettings.EmailSettings.EmailToken}");
                var httpResponse = await client.PostAsync(_appSettings.EmailSettings.EmailWebserviceUrl, content);
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
