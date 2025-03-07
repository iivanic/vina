namespace vina.Server.Config;

public class AppSettingsOptions
{
    public const string AppSettings="AppSettings";
    public string DatabaseName { get; set; } = "";
    public EmailSettings EmailSettings { get; set; } = new EmailSettings();
    public JWTSettings JWT { get; set; } = new JWTSettings();

}
public class EmailSettings
{
    public string EmailWebserviceUrl { get; set; } = "";
    public string EmailSender { get; set; } = "";
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
    public string RedirectUri { get; set; } = "";
}
public class JWTSettings
{
    public string Key { get; set; } = "";
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
}
