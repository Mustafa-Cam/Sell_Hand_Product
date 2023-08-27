namespace sellhandproduct.Models
{
    public class BasketViewModel
    {
        public int Id { get; set; }

        public byte[] ImageData { get; set; }
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public int ProductCount { get; set; }

        public string SellerId { get; set; }
    }
}
