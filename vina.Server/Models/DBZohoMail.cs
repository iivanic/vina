using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models;

[Table("zoho_mail")]
public class DBZohoMail
{
    [Key]
    public int Id { get; set; }=1;
    public string? AuthorizationCode { get; set; }="";
    public string? AuthorizationLocation { get; set; }="";
    public string? AuthorizationAccountsServer { get; set; }="";
    public DateTime? AuthorizationCodeTimestamp { get; set; }
    public string? AccessToken { get; set; }="";
    public string? AccessTokenType { get; set; }="";
    public DateTime? AccessTokenTimestamp { get; set; }
    public int? AccessTokenExpiresIn { get; set; }
    public DateTime? AccessTokenValidUntil { get; set; }
    public string? RefreshToken { get; set; }="";
    public string? ApiDomain { get; set; } = "";
    // Not used by DBHelp directly
    public const string SelectText = "select * from zoho_mail ;";
    public const string SelectSingleText = "select * from zoho_mail where id=@id ;";
    public const string UpdateText = "update zoho_mail set authorization_code=@authorization_code,  authorization_location = @authorization_location, authorization_accountsServer = @authorization_accountsServer,authorization_code_timestamp=@authorization_code_timestamp, access_token=@access_token, access_token_type=@access_token_type, access_token_timestamp=@access_token_timestamp, access_token_expires_in=@access_token_expires_in, access_token_valid_until=@access_token_valid_until, refresh_token=@refresh_token, api_domain = @api_domain where id=@id returning *;";
    public const string InsertText = "insert into zoho_mail ( id, authorization_code, authorization_location, authorization_accountsServer, authorization_code_timestamp, access_token, access_token_type, access_token_timestamp, access_token_expires_in, access_token_valid_until, refresh_token, api_domain) values(@id, @authorization_code, @authorization_location, @authorization_accountsServer, @authorization_code_timestamp, @access_token, @access_token_type, @access_token_timestamp, @access_token_expires_in, @access_token_valid_until, @refresh_token, @api_domain)  returning *;";
    public const string DeleteText = "delete from zoho_mail where id=@id;";

}

