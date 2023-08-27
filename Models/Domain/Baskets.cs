namespace sellhandproduct.Models.Domain
{
    public class Baskets
    {

        public Baskets()
        {
            Product = new List<Products>(); // Default olarak Product koleksiyonunu boş bir liste olarak oluştur
        }
        public int Id { get; set; }
        

        public string UserId { get; set; }

        public ApplicationUser User { get; set; } 

         public  ICollection<Products>? Product { get; set; }
    }
}
