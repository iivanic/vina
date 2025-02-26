namespace vina.Server.Services
{
    public class AuthMessageSenderOptions
    {
        public string MailJetKey { get; set; } = "754b9bf243c7381a49af56b34bdbdb69";
        public string MailJetApiSecret { get; set; } = "4af7123f46574677193149d07fb5131b";
        public string FromMail { get; set; } = "info@vina-ivanic.hr";
        public string FromName{ get; set; } = "Ivanic Winery";
        public string CustomId { get; set; } = Guid.NewGuid().ToString();
    }
}
