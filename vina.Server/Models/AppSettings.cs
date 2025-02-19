namespace vina.Server.Config
{
    public class AppSettings
    {
        public string DatabaseName { get; set; } = "";  
        public EmailSettings EmailSettings { get; set; }=new EmailSettings();
    }
    public class EmailSettings
    {
        public string EmailWebserviceUrl { get; set; } = "";
        public string EmailSender { get; set; } = "";
        public string EmailToken { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
    }
}