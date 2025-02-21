using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models;

[Table("translations")]
public class DBTranslation
{
	[Key]
	public int Id { get; set; }
	public string Key { get; set; } = string.Empty;
	public string Content { get; set; } = string.Empty;
	public string Lang { get; set; } = string.Empty;
	// Not used by DBHelp directly
	public const string SelectText = "select * from translations ;";
	public const string SelectSingleText = "select * from translations where id=@id;";
	public const string SelectKeyLangText = "select * from translations where key=@key and lang=@lang;";
	public const string UpdateText = "update translations set key=@key, content=@content, lang=@lang where id=@id returning *;";
	public const string InsertText = "insert into translations (key, content, lang) values(@key, @content, @lang)  returning *;";
	public const string DeleteText = "delete from translations where id=@id;";

}


