using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Models
{
	public class TranslatedProduct:DBProduct
	{
		new public const string SelectText = @"
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
		new public const string SelectSingleText = @"
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

	}

}
