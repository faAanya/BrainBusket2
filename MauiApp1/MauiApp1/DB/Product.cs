using SQLite;

namespace MauiApp1.DB
{
    [Table("product")]
    public class Product
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("product_name")]

        public string? Name { get; set; }

        [Column("product_state")]

        public bool IsBought { get; set; } = false;


        [Column("product_category")]
        public Color? ProductCategoryColor { get; set; }

        [Column("product_description")]
        public string? Description { get; set; }

        [Column("product_amount")]
        public int? Amount { get; set; } = 1;

        public Product Clone() => MemberwiseClone() as Product;


        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return (false, $"{nameof(Name)} Название продукта обязательно");
            }
            else if (Amount < 0) {
                return (false, $"{nameof(Amount)} should be greater than 0)");
            }
            return (true, null);
        }
    }
}
