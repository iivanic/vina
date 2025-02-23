using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models;

[Table("categories")]
public class DBCategory
{
	[Key]
	public int Id { get; set; }
	public string NameTranslationKey { get; set; } = string.Empty;
	// Collection of class that references via FK
	public List<DBProduct> Products { get; set; } = new List<DBProduct>();
	// Not used by DBHelp directly
	public const string SelectText = "select * from categories ;";
	public const string SelectSingleText = "select * from categories where id=@id;";
	public const string UpdateText = "update categories set name_translation_key=@name_translation_key where id=@id returning *;";
	public const string InsertText = "insert into categories (name_translation_key) values(@name_translation_key)  returning *;";
	public const string DeleteText = "delete from categories where id=@id;";

}



