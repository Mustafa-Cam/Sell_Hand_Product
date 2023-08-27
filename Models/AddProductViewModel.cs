namespace sellhandproduct.Models
{
    public class AddProductViewModel
    {
       public int ProductId { get; set;} 
       public string ProductName { get; set; }
       public double ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string SellerId { get; set; }

        public IFormFile ImageFile { get; set; }


    }
}
