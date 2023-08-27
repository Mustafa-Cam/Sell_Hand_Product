//using sellhandproduct.Models.intermediate;

namespace sellhandproduct.Models.Domain
{
    public class Products 
    {

        public int Id { get; set; } 

        public byte[] ImageData { get; set; }
        public string ProductName { get; set; }

        public double ProductPrice { get; set; }

        public int ProductCount { get; set; }

        public string SellerId { get; set; }

        public virtual ICollection<Baskets>? Basket { get; set; }


        // Diğer özellikler... 

        public void SetImage(IFormFile file) 
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    ImageData = memoryStream.ToArray();
                }
            }
    }
}
