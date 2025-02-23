using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace vina.Server.Models;

[Table("logs")]
public class DBLog
{
	[Key]
	public int Id {get; set;}
	public DateTime? LogTimestamp {get; set;}
	public string? LogLevel {get; set;}
	public string? CategoryName {get; set;}
	public string? Message {get; set;}
	public string? Exception {get; set;}
	// Not used by DBHelp directly
	public const string SelectText = "select * from logs ;";
	public const string SelectSingleText = "select * from logs where id=@id;";
	public const string UpdateText = "update logs set log_timestamp=@log_timestamp, log_level=@log_level, category_name=@category_name, message=@message, exception=@exception where id=@id returning *;";
	public const string InsertText = "insert into logs (log_timestamp, log_level, category_name, message, exception) values(@log_timestamp, @log_level, @category_name, @message, @exception)  returning *;";
	public const string DeleteText = "delete from logs where id=@id;";

}
