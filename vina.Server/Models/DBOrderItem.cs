using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models;

[Table("order_items")]
public class DBOrderItem
{
	[Key]
	public int Id { get; set; }
	[ForeignKey("DBOrder")]
	// property for refernced object
	public int OrderId { get; set; }
	[NotMapped]
	public DBOrder? DBOrder { get; set; }
	[ForeignKey("DBProduct")]
	// property for refernced object
	public int ProductId { get; set; }
	[NotMapped]
	public DBProduct? DBProduct { get; set; }
	public double UnitPrice { get; set; }
	public int Quantity { get; set; }
	// Not used by DBHelp directly
	public const string SelectText = "select * from order_items ;";
	public const string SelectSingleText = "select * from order_items where id=@id;";
	public const string UpdateText = "update order_items set order_id=@order_id, product_id=@product_id, unit_price=@unit_price, quantity=@quantity where id=@id returning *;";
	public const string InsertText = "insert into order_items (order_id, product_id, unit_price, quantity) values(@order_id, @product_id, @unit_price, @quantity)  returning *;";
	public const string DeleteText = "delete from order_items where id=@id;";

}

