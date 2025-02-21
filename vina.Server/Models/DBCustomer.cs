using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models;

[Table("customers")]
public class DBCustomer
{
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    // Not used by DBHelp directly
    public const string SelectText = "select * from customers ;";
    public const string SelectSingleText = "select * from customers where id=@id;";
    public const string UpdateText = "update customers set email=@email, is_admin=@is_admin where id=@id returning *;";
    public const string InsertText = "insert into customers (email, is_admin) values(@email, @is_admin)  returning *;";
    public const string DeleteText = "delete from customers where id=@id;";

}

