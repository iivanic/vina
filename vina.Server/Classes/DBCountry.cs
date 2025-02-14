using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace vina.Server.Classes
{
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

    [Table("translations")]
    public class DBToken
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Lang { get; set; } = string.Empty;
        // Not used by DBHelp directly
        public const string SelectText = "select * from translations ;";
        public const string SelectSingleText = "select * from translations where id=@id;";
        public const string UpdateText = "update translations set key=@key, content=@content, lang=@lang where id=@id returning *;";
        public const string InsertText = "insert into translations (key, content, lang) values(@key, @content, @lang)  returning *;";
        public const string DeleteText = "delete from translations where id=@id;";

    }

    [Table("products")]
    public class DBTranslation
    {
        [Key]
        public int Id { get; set; }
        public string NameTranslationKey { get; set; } = string.Empty;
        public string? DescriptionTranslationKey { get; set; }
        public double Price { get; set; }
        public int MaxOrder { get; set; }
        public bool Avalaible { get; set; }
        public bool Published { get; set; }
        [ForeignKey("DBProduct")]
        // property for refernced object
        public int CategoryId { get; set; }
        [NotMapped]
        public DBProduct DBProduct { get; set; }
        public string? FullTranslationKey { get; set; }
        // Not used by DBHelp directly
        public const string SelectText = "select * from products ;";
        public const string SelectSingleText = "select * from products where id=@id;";
        public const string UpdateText = "update products set name_translation_key=@name_translation_key, description_translation_key=@description_translation_key, price=@price, max_order=@max_order, avalaible=@avalaible, published=@published, category_id=@category_id, full_translation_key=@full_translation_key where id=@id returning *;";
        public const string InsertText = "insert into products (name_translation_key, description_translation_key, price, max_order, avalaible, published, category_id, full_translation_key) values(@name_translation_key, @description_translation_key, @price, @max_order, @avalaible, @published, @category_id, @full_translation_key)  returning *;";
        public const string DeleteText = "delete from products where id=@id;";

    }

    [Table("countries")]
    public class DBOrder
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Lang { get; set; } = string.Empty;
        // Not used by DBHelp directly
        public const string SelectText = "select * from countries ;";
        public const string SelectSingleText = "select * from countries where id=@id;";
        public const string UpdateText = "update countries set name=@name, is_active=@is_active, lang=@lang where id=@id returning *;";
        public const string InsertText = "insert into countries (name, is_active, lang) values(@name, @is_active, @lang)  returning *;";
        public const string DeleteText = "delete from countries where id=@id;";

    }

    [Table("order_status")]
    public class DBOrderItem
    {
        [Key]
        public int Id { get; set; }
        public string StatusTranslationKey { get; set; } = string.Empty;
        public bool IsClosed { get; set; }
        // Not used by DBHelp directly
        public const string SelectText = "select * from order_status ;";
        public const string SelectSingleText = "select * from order_status where id=@id;";
        public const string UpdateText = "update order_status set status_translation_key=@status_translation_key, is_closed=@is_closed where id=@id returning *;";
        public const string InsertText = "insert into order_status (status_translation_key, is_closed) values(@status_translation_key, @is_closed)  returning *;";
        public const string DeleteText = "delete from order_status where id=@id;";

    }

    [Table("categories")]
    public class DBProduct
    {
        [Key]
        public int Id { get; set; }
        public string NameTranslationKey { get; set; } = string.Empty;
        // Collection of class that references via FK
        public List<DBTranslation> Products { get; set; } = new List<DBTranslation>();
        // Not used by DBHelp directly
        public const string SelectText = "select * from categories ;";
        public const string SelectSingleText = "select * from categories where id=@id;";
        public const string UpdateText = "update categories set name_translation_key=@name_translation_key where id=@id returning *;";
        public const string InsertText = "insert into categories (name_translation_key) values(@name_translation_key)  returning *;";
        public const string DeleteText = "delete from categories where id=@id;";

    }

}
