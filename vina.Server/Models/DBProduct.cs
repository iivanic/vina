using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models;

[Table("products")]
public class DBProduct
{
	[Key]
	public int Id { get; set; }
	public string NameK { get; set; } = string.Empty;
	public string? DescriptionK { get; set; }
	public string? FullK { get; set; }
	public double Price { get; set; }
	public int MaxOrder { get; set; }
	public bool Avalaible { get; set; }
	public bool Published { get; set; }
	[ForeignKey("DBCategory")]
	// property for refernced object
	public int CategoryId { get; set; }
	[NotMapped]
	public DBCategory? DBCategory { get; set; }
	// Collection of class that references via FK
	public List<DBOrderItem> OrderItems { get; set; } = new List<DBOrderItem>();
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
	public const string SelectSingleText = @"
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


