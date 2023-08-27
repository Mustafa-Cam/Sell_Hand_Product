using Microsoft.AspNetCore.Identity;

namespace sellhandproduct.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 

        public int SellerNo { get; set; }

        }
}


