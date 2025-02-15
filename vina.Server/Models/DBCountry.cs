using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models
{
	[Table("countries")]
	public class DBCountry
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public bool IsActive { get; set; }
		public string Lang { get; set; } = string.Empty;
		// Collection of class that references via FK
		public List<DBOrder> Orders { get; set; } = new List<DBOrder>();
		// Not used by DBHelp directly
		public const string SelectText = "select * from countries ;";
		public const string SelectSingleText = "select * from countries where id=@id;";
		public const string UpdateText = "update countries set name=@name, is_active=@is_active, lang=@lang where id=@id returning *;";
		public const string InsertText = "insert into countries (name, is_active, lang) values(@name, @is_active, @lang)  returning *;";
		public const string DeleteText = "delete from countries where id=@id;";

	}

}
