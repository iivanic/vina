using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models
{
	[Table("tokens")]
	public class DBToken
	{
		[Key]
		public int Id { get; set; }
		public string Email { get; set; } = string.Empty;
		public DateTime ValidFrom { get; set; }
		public DateTime ValidUntil { get; set; }
		public string Challenge { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public bool IsRevoked { get; set; }
		// Not used by DBHelp directly
		public const string SelectText = "select * from tokens ;";
		public const string SelectSingleText = "select * from tokens where id=@id;";
		public const string UpdateText = "update tokens set email=@email, valid_from=@valid_from, valid_until=@valid_until, challenge=@challenge, token=@token, is_revoked=@is_revoked where id=@id returning *;";
		public const string InsertText = "insert into tokens (email, valid_from, valid_until, challenge, token, is_revoked) values(@email, @valid_from, @valid_until, @challenge, @token, @is_revoked)  returning *;";
		public const string DeleteText = "delete from tokens where id=@id;";

	}


}
