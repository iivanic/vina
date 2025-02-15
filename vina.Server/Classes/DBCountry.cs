using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Classes
{
	[Table("customers")]
	public class DBCustomer
	{
		[Key]
		public int Id {get; set;}
		public string Email {get; set;} = string.Empty;
		public bool IsAdmin {get; set;}
		// Not used by DBHelp directly
		public const string SelectText = "select * from customers ;";
		public const string SelectSingleText = "select * from customers where id=@id;";
		public const string UpdateText = "update customers set email=@email, is_admin=@is_admin where id=@id returning *;";
		public const string InsertText = "insert into customers (email, is_admin) values(@email, @is_admin)  returning *;";
		public const string DeleteText = "delete from customers where id=@id;";

	}

	[Table("tokens")]
	public class DBToken
	{
		[Key]
		public int Id {get; set;}
		public string Email {get; set;} = string.Empty;
		public DateTime ValidFrom {get; set;}
		public DateTime ValidUntil {get; set;}
		public string Challenge {get; set;} = string.Empty;
		public string Token {get; set;} = string.Empty;
		public bool IsRevoked {get; set;}
		// Not used by DBHelp directly
		public const string SelectText = "select * from tokens ;";
		public const string SelectSingleText = "select * from tokens where id=@id;";
		public const string UpdateText = "update tokens set email=@email, valid_from=@valid_from, valid_until=@valid_until, challenge=@challenge, token=@token, is_revoked=@is_revoked where id=@id returning *;";
		public const string InsertText = "insert into tokens (email, valid_from, valid_until, challenge, token, is_revoked) values(@email, @valid_from, @valid_until, @challenge, @token, @is_revoked)  returning *;";
		public const string DeleteText = "delete from tokens where id=@id;";

	}

	[Table("translations")]
	public class DBTranslation
	{
		[Key]
		public int Id {get; set;}
		public string Key {get; set;} = string.Empty;
		public string Content {get; set;} = string.Empty;
		public string Lang {get; set;} = string.Empty;
		// Not used by DBHelp directly
		public const string SelectText = "select * from translations ;";
		public const string SelectSingleText = "select * from translations where id=@id;";
		public const string UpdateText = "update translations set key=@key, content=@content, lang=@lang where id=@id returning *;";
		public const string InsertText = "insert into translations (key, content, lang) values(@key, @content, @lang)  returning *;";
		public const string DeleteText = "delete from translations where id=@id;";

	}

	[Table("orders")]
	public class DBOrder
	{
		[Key]
		public int Id {get; set;}
		public DateTime OrderTime {get; set;}
		public DateTime? PaymentRecievedTime {get; set;}
		public DateTime? PackageSentTime {get; set;}
		public DateTime? PackageRecievededByCustomerTime {get; set;}
		public double Amount {get; set;}
		public string? PhoneForDelivery {get; set;}
		public string? State {get; set;}
		public string? City {get; set;}
		public string? Address1 {get; set;}
		public string? Address2 {get; set;}
		public string? Comment {get; set;}
		[ForeignKey("DBOrderStatus")]
		// property for refernced object
		public int StatusId {get; set;}
		[NotMapped]
		public DBOrderStatus? DBOrderStatus {get; set;}
		[ForeignKey("DBCountry")]
		// property for refernced object
		public int CountryId {get; set;}
		[NotMapped]
		public DBCountry? DBCountry {get; set;}
		// Collection of class that references via FK
		public List<DBOrderItem> OrderItems {get; set;} = new List<DBOrderItem>();
		// Not used by DBHelp directly
		public const string SelectText = "select * from orders ;";
		public const string SelectSingleText = "select * from orders where id=@id;";
		public const string UpdateText = "update orders set order_time=@order_time, payment_recieved_time=@payment_recieved_time, package_sent_time=@package_sent_time, package_recieveded_by_customer_time=@package_recieveded_by_customer_time, amount=@amount, phone_for_delivery=@phone_for_delivery, state=@state, city=@city, address1=@address1, address2=@address2, comment=@comment, status_id=@status_id, country_id=@country_id where id=@id returning *;";
		public const string InsertText = "insert into orders (order_time, payment_recieved_time, package_sent_time, package_recieveded_by_customer_time, amount, phone_for_delivery, state, city, address1, address2, comment, status_id, country_id) values(@order_time, @payment_recieved_time, @package_sent_time, @package_recieveded_by_customer_time, @amount, @phone_for_delivery, @state, @city, @address1, @address2, @comment, @status_id, @country_id)  returning *;";
		public const string DeleteText = "delete from orders where id=@id;";

	}

	[Table("order_items")]
	public class DBOrderItem
	{
		[Key]
		public int Id {get; set;}
		[ForeignKey("DBOrder")]
		// property for refernced object
		public int OrderId {get; set;}
		[NotMapped]
		public DBOrder? DBOrder {get; set;}
		[ForeignKey("DBProduct")]
		// property for refernced object
		public int ProductId {get; set;}
		[NotMapped]
		public DBProduct? DBProduct {get; set;}
		public double UnitPrice {get; set;}
		public int Quantity {get; set;}
		// Not used by DBHelp directly
		public const string SelectText = "select * from order_items ;";
		public const string SelectSingleText = "select * from order_items where id=@id;";
		public const string UpdateText = "update order_items set order_id=@order_id, product_id=@product_id, unit_price=@unit_price, quantity=@quantity where id=@id returning *;";
		public const string InsertText = "insert into order_items (order_id, product_id, unit_price, quantity) values(@order_id, @product_id, @unit_price, @quantity)  returning *;";
		public const string DeleteText = "delete from order_items where id=@id;";

	}

	[Table("products")]
	public class DBProduct
	{
		[Key]
		public int Id {get; set;}
		public string NameK {get; set;} = string.Empty;
		public string? DescriptionK {get; set;}
		public string? FullK {get; set;}
		public double Price {get; set;}
		public int MaxOrder {get; set;}
		public bool Avalaible {get; set;}
		public bool Published {get; set;}
		[ForeignKey("DBCategory")]
		// property for refernced object
		public int CategoryId {get; set;}
		[NotMapped]
		public DBCategory? DBCategory {get; set;}
		// Collection of class that references via FK
		public List<DBOrderItem> OrderItems {get; set;} = new List<DBOrderItem>();
		// Not used by DBHelp directly
		public const string SelectText = @"
					select
						p.Id, 
						t1.content as name_k,
						t2.content as description_k,
						t3.content as full_k,
						p.price,
						p.max_order,
						avalaible,
						published,
						category_id
					from products p
					left join translations t1 on p.name_k = t1.key
					left join translations t2 on p.description_k = t2.key
					left join translations t3 on p.full_k = t3.key
					where 
					t1.lang = t2.lang
					and t2.lang = t3.lang
					and t3.lang = @lang
					and published = true;";
		public const string SelectSingleText =  @"
					select
						p.Id, 
						t1.content as name_k,
						t2.content as description_k,
						t3.content as full_k,
						p.price,
						p.max_order,
						avalaible,
						published,
						category_id
					from products p
					left join translations t1 on p.name_k = t1.key
					left join translations t2 on p.description_k = t2.key
					left join translations t3 on p.full_k = t3.key
					where 
					t1.lang = t2.lang
					and t2.lang = t3.lang
					and t3.lang = @lang
					and published = true
					and p.id=@id;";
		public const string UpdateText = "update products set name_translation_key=@name_translation_key, description_translation_key=@description_translation_key, price=@price, max_order=@max_order, avalaible=@avalaible, published=@published, category_id=@category_id, full_translation_key=@full_translation_key where id=@id returning *;";
		public const string InsertText = "insert into products (name_translation_key, description_translation_key, price, max_order, avalaible, published, category_id, full_translation_key) values(@name_translation_key, @description_translation_key, @price, @max_order, @avalaible, @published, @category_id, @full_translation_key)  returning *;";
		public const string DeleteText = "delete from products where id=@id;";

	}

	[Table("countries")]
	public class DBCountry
	{
		[Key]
		public int Id {get; set;}
		public string Name {get; set;} = string.Empty;
		public bool IsActive {get; set;}
		public string Lang {get; set;} = string.Empty;
		// Collection of class that references via FK
		public List<DBOrder> Orders {get; set;} = new List<DBOrder>();
		// Not used by DBHelp directly
		public const string SelectText = "select * from countries ;";
		public const string SelectSingleText = "select * from countries where id=@id;";
		public const string UpdateText = "update countries set name=@name, is_active=@is_active, lang=@lang where id=@id returning *;";
		public const string InsertText = "insert into countries (name, is_active, lang) values(@name, @is_active, @lang)  returning *;";
		public const string DeleteText = "delete from countries where id=@id;";

	}

	[Table("order_status")]
	public class DBOrderStatus
	{
		[Key]
		public int Id {get; set;}
		public string StatusTranslationKey {get; set;} = string.Empty;
		public bool IsClosed {get; set;}
		// Collection of class that references via FK
		public List<DBOrder> Orders {get; set;} = new List<DBOrder>();
		// Not used by DBHelp directly
		public const string SelectText = "select * from order_status ;";
		public const string SelectSingleText = "select * from order_status where id=@id;";
		public const string UpdateText = "update order_status set status_translation_key=@status_translation_key, is_closed=@is_closed where id=@id returning *;";
		public const string InsertText = "insert into order_status (status_translation_key, is_closed) values(@status_translation_key, @is_closed)  returning *;";
		public const string DeleteText = "delete from order_status where id=@id;";

	}

	[Table("categories")]
	public class DBCategory
	{
		[Key]
		public int Id {get; set;}
		public string NameTranslationKey {get; set;} = string.Empty;
		// Collection of class that references via FK
		public List<DBProduct> Products {get; set;} = new List<DBProduct>();
		// Not used by DBHelp directly
		public const string SelectText = "select * from categories ;";
		public const string SelectSingleText = "select * from categories where id=@id;";
		public const string UpdateText = "update categories set name_translation_key=@name_translation_key where id=@id returning *;";
		public const string InsertText = "insert into categories (name_translation_key) values(@name_translation_key)  returning *;";
		public const string DeleteText = "delete from categories where id=@id;";

	}


}
