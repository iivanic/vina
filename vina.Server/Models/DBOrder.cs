using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models
{
	[Table("orders")]
	public class DBOrder
	{
		[Key]
		public int Id { get; set; }
		public DateTime OrderTime { get; set; }
		public DateTime? PaymentRecievedTime { get; set; }
		public DateTime? PackageSentTime { get; set; }
		public DateTime? PackageRecievededByCustomerTime { get; set; }
		public double Amount { get; set; }
		public string? PhoneForDelivery { get; set; }
		public string? State { get; set; }
		public string? City { get; set; }
		public string? Address1 { get; set; }
		public string? Address2 { get; set; }
		public string? Comment { get; set; }
		[ForeignKey("DBOrderStatus")]
		// property for refernced object
		public int StatusId { get; set; }
		[NotMapped]
		public DBOrderStatus? DBOrderStatus { get; set; }
		[ForeignKey("DBCountry")]
		// property for refernced object
		public int CountryId { get; set; }
		[NotMapped]
		public DBCountry? DBCountry { get; set; }
		// Collection of class that references via FK
		public List<DBOrderItem> OrderItems { get; set; } = new List<DBOrderItem>();
		// Not used by DBHelp directly
		public const string SelectText = "select * from orders ;";
		public const string SelectSingleText = "select * from orders where id=@id;";
		public const string UpdateText = "update orders set order_time=@order_time, payment_recieved_time=@payment_recieved_time, package_sent_time=@package_sent_time, package_recieveded_by_customer_time=@package_recieveded_by_customer_time, amount=@amount, phone_for_delivery=@phone_for_delivery, state=@state, city=@city, address1=@address1, address2=@address2, comment=@comment, status_id=@status_id, country_id=@country_id where id=@id returning *;";
		public const string InsertText = "insert into orders (order_time, payment_recieved_time, package_sent_time, package_recieveded_by_customer_time, amount, phone_for_delivery, state, city, address1, address2, comment, status_id, country_id) values(@order_time, @payment_recieved_time, @package_sent_time, @package_recieveded_by_customer_time, @amount, @phone_for_delivery, @state, @city, @address1, @address2, @comment, @status_id, @country_id)  returning *;";
		public const string DeleteText = "delete from orders where id=@id;";

	}

}
