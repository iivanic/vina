using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models
{
	[Table("order_status")]
	public class DBOrderStatus
	{
		[Key]
		public int Id { get; set; }
		public string StatusTranslationKey { get; set; } = string.Empty;
		public bool IsClosed { get; set; }
		// Collection of class that references via FK
		public List<DBOrder> Orders { get; set; } = new List<DBOrder>();
		// Not used by DBHelp directly
		public const string SelectText = "select * from order_status ;";
		public const string SelectSingleText = "select * from order_status where id=@id;";
		public const string UpdateText = "update order_status set status_translation_key=@status_translation_key, is_closed=@is_closed where id=@id returning *;";
		public const string InsertText = "insert into order_status (status_translation_key, is_closed) values(@status_translation_key, @is_closed)  returning *;";
		public const string DeleteText = "delete from order_status where id=@id;";

	}


}
