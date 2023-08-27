namespace sellhandproduct.Models.Domain
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int ProductId { get; set; }
        public virtual Products Product { get; set;}
    }
}
