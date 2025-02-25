
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace vina.Server.Services
{

    public class MailJetEmailService : IEmailSender
    {
        private readonly ILogger _logger;

        public MailJetEmailService(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<MailJetEmailService> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.MailJetKey))
            {
                throw new Exception("Null MailJetKey");
            }
            if (string.IsNullOrEmpty(Options.MailJetApiSecret))
            {
                throw new Exception("Null MailJetApiSecret");
            }
            await Execute(Options.MailJetKey, Options.MailJetApiSecret,Options.FromMail, Options.FromName, subject, message, toEmail, Options.CustomId);
        }

        public async Task Execute(string apiKey, string apiSecret, string fromMail, string fromName, string subject, string message, string toEmail, string customId)
        {
            MailjetClient client = new MailjetClient(
                apiKey,
                apiSecret
                );

            MailjetRequest request = new MailjetRequest
            {
              //  Resource = Send.Resource,
                Resource = SendV31.Resource,
            }
             .Property(Send.Messages, new JArray {
                 new JObject {
                  {
                   "From",
                   new JObject {
                    {"Email", fromMail},
                    {"Name", ""}
                   }
                  }, {
                   "To",
                   new JArray {
                    new JObject {
                     {
                      "Email",
                      toEmail
                     }, {
                      "Name",
                      ""
                     }
                    }
                   }
                  }, {
                   "Subject",
                   subject
                     }, {
                         "TextPart",
                   Regex.Replace(message, "<(.|\\n)*?>", string.Empty)
                }, {
                   "HTMLPart",
                   message
                  }, {
                   "CustomID",
                   customId
                  }
                 }
             });
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                _logger.LogInformation(response.GetData().ToString());
            }
            else
            {
                _logger.LogInformation(string.Format("StatusCode: {0}\n", response.StatusCode));
                _logger.LogInformation(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                _logger.LogInformation(response.GetData().ToString());
                _logger.LogInformation(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

        }
    }
}
