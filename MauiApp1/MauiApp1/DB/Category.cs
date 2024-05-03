using SQLite;


namespace MauiApp1.DB
{
    [Table("category")]
   public class Category
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int CategoryId { get; set; }

        [Column("category_name")]

        public string? CategoryName { get; set; }

        [Column("category_name")]

        public Color? CategoryColor { get; set; }

        public Category Clone() => MemberwiseClone() as Category;
    }
}
