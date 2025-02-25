namespace vina.Server.Services
{
    public class AuthMessageSenderOptions
    {
        public string MailJetKey { get; set; } = "";
        public string MailJetApiSecret { get; set; } = "";
        public string FromMail { get; set; } = "info@vina-ivanic.hr";
        public string FromName{ get; set; } = "Ivanic Winery";
        public string CustomId { get; set; } = Guid.NewGuid().ToString();
    }
}
